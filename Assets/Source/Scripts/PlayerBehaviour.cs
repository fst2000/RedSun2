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
  [SerializeField] float walkAimSpeed;
  [SerializeField] float crouchAimSpeed;
  new Rigidbody rigidbody;
  CapsuleCollider capsuleCollider;

  IEvent fixedUpdate;
  IEvent update;
  IHumanStatus status;
  IHumanSize humanSize;
  IAnimator humanAnimator;
  IMoveSystem playerMoveSystem;
  IVector3 inputMoveV3;
  IVector3 moveVector3;
  FloatFunc humanSizeFunc;
  FloatFunc moveSpeed;
  FloatFunc animBlend;
  Vector2Func animBlendXY;
  HumanStates playerStates;
  StateMachine animStateMachine;
  StateMachine moveStateMachine;
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
      update = new Event();
      status = new PlayerStatus();
      humanSize = new ColliderHumanSize(capsuleCollider);
      humanAnimator = new UnityAnimator(animator);
      inputMoveV3 = new InputMoveVector3();
      moveSpeed = fAction =>
      {
        status.IsCrouching()(crouching =>
        {
          status.IsArmed()(armed =>
          {
            status.IsAiming()(aiming =>
            {
              if(crouching && aiming && armed) fAction(crouchAimSpeed);
              else if(aiming && armed) fAction(walkAimSpeed);
              else if(crouching) fAction(crouchSpeed);
              else fAction(runSpeed);
            });
          });
        });
      };
      moveVector3 = new ScaledVector3(new FlatVector3(new TransformDirectionVector3(inputMoveV3, camera)), moveSpeed);
      humanSizeFunc = fAction =>
      {
        status.IsCrouching()(crouching => fAction(crouching ? 1.2f : 1.8f));
      };
      animBlend = fAction => inputMoveV3.Accept(v3 => fAction(v3.magnitude));
      animBlendXY = v2Action => inputMoveV3.Accept(v3 => v2Action(new Vector2(v3.x, v3.z)));
      playerMoveSystem = new RigidbodyMoveSystem(rigidbody, fixedUpdate);
      playerStates = new HumanStates(humanAnimator, status, playerMoveSystem, moveVector3, animBlend, animBlendXY, transform, camera);
      animStateMachine = new StateMachine(playerStates.animRun());
      rotStateMachine = new StateMachine(playerStates.rotMove());
      moveStateMachine = new StateMachine(playerStates.moveInput());

      new AutomatBullet(new BulletPositionVector3(Vector3.zero, transform.forward, 1), update);
  }
  void Update()
  {
    update.Call();
      animStateMachine.Update();
      rotStateMachine.Update();
      moveStateMachine.Update();
      humanSizeFunc(f => humanSize.Accept(f));
  }
  void FixedUpdate()
  {
      fixedUpdate.Call();
  }
  void LateUpdate()
  {

  }
}
