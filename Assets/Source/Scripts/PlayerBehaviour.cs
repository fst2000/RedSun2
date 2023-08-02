using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] new Transform camera;
    [SerializeField] Animator animator;
    [SerializeField] float runSpeed;
    [SerializeField] float crouchSpeed;
    new Rigidbody rigidbody;
    CapsuleCollider capsuleCollider;

    IEvent fixedUpdate;
    IHumanStatus status;
    IAnimator humanAnimator;
    IVector3 inputMoveV3;
    IVector3 moveVector3;
    Action<float> moveSpeedAction;
    FloatFunc animBlend;
    Vector2Func animBlendXY;
    IMoveSystem playerMoveSystem;

    HumanState animRun;
    HumanState animCrouch;
    HumanState animWalkAim;
    HumanState animCrouchAim;
    HumanState animRunArmed;
    HumanState animCrouchArmed;
    StateMachine animStateMachine;

    HumanState moveInput;
    StateMachine moveStateMachine;

    HumanState rotMove;
    HumanState rotAim;
    StateMachine rotStateMachine;
   void Start()
   {
     rigidbody = gameObject.AddComponent<Rigidbody>();
     rigidbody.freezeRotation = true;
     rigidbody.mass = 60;
     capsuleCollider = gameObject.AddComponent<CapsuleCollider>();
     capsuleCollider.height = 1.8f;
     capsuleCollider.radius = 0.5f;
     capsuleCollider.center = new Vector3(0, 0.9f, 0);

     fixedUpdate = new Event();
     status = new PlayerStatus();
     humanAnimator = new UnityAnimator(animator);
     inputMoveV3 = new InputMoveVector3();
     moveVector3 = new FlatVector3(new TransformDirectionVector3(inputMoveV3, camera));
     IVector3 speedMoveVector3 = moveVector3;
     moveSpeedAction = f => speedMoveVector3 = new ScaledVector3(moveVector3, f);
     
     animBlend = fAction => inputMoveV3.Accept(v3 => fAction(v3.magnitude));
     animBlendXY = v2Action => inputMoveV3.Accept(v3 => v2Action(new Vector2(v3.x, v3.z)));
     playerMoveSystem = new RigidbodyMoveSystem(rigidbody, fixedUpdate);

     animRun = () => new AnimRunState(humanAnimator, animBlend, status, animCrouch, animWalkAim, animRunArmed);
     animCrouch = () => new AnimCrouchState(humanAnimator, animBlend, status, animRun, animCrouchAim, animCrouchArmed);
     animWalkAim = () => new AnimWalkAimState(humanAnimator, animBlendXY, status, animCrouchAim, animRun);
     animCrouchAim = () => new AnimCrouchAimState(humanAnimator, animBlendXY, status, animWalkAim, animCrouch);
     animRunArmed = () => new AnimRunArmedState(humanAnimator, animBlend, status, animCrouchArmed, animWalkAim, animRun);
     animCrouchArmed = () => new AnimCrouchArmedState(humanAnimator, animBlend, status, animRun, animCrouchAim, animCrouch);
     animStateMachine = new StateMachine(animRun());

     rotMove = () => new RotMoveState(moveVector3, transform, status, rotAim);
     rotAim = () => new RotAimState(transform, camera, status, rotMove);
     rotStateMachine = new StateMachine(rotMove());

     moveInput = () => new MoveInputState(moveVector3, playerMoveSystem);
     moveStateMachine = new StateMachine(moveInput());

   }
   void Update()
   {
    animStateMachine.Update();
    rotStateMachine.Update();
    moveStateMachine.Update();
    moveSpeedAction(5);
   }
   void FixedUpdate()
   {
    fixedUpdate.Call();
   }
   void LateUpdate()
   {

   }
    
}
public delegate IHumanState HumanState();
