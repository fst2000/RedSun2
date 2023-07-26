using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] float runSpeed;
    [SerializeField] float walkSpeed;
    [SerializeField] float crouchSpeed;
    new Transform camera;
    new Rigidbody rigidbody;
    IHumanState currentState;
    HumanState runState;
    HumanState walkAimState;
    IAnimator humanAnimator;
    KeyboardMoveInput moveInput;
    RotationFunc moveRotationFunc;
    RotationFunc moveAimRotationFunc;
    Vector3Func moveVector3Func;
    Vector2Func walkAimAnimatorFunc;
    FloatFunc runAnimatorFunc;
    BoolFunc isAimingFunc;
    Action runStartAction;
    Action runStateUpdateAction;
    Action walkAimStartAction;
    Action walkAimUpdateAction;
    void Start()
    {
        camera = Camera.main.transform;

        rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.mass = 60;
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rigidbody.freezeRotation = true;

        humanAnimator = new UnityAnimator(animator);
        moveInput = new KeyboardMoveInput();
        moveRotationFunc = qAction => moveVector3Func(v3 =>
        {
            if(v3.magnitude > 0.01f) qAction(Quaternion.LookRotation(v3));
        });
        moveAimRotationFunc = qAction => 
        {
            Vector3 look = camera.transform.forward;
            look.y = 0;
            qAction(Quaternion.LookRotation(look));
        };
        moveVector3Func = v3Action =>
        {
            moveInput.Accept(v2 =>
            {
                Vector3 inputV3 = camera.TransformDirection(new Vector3(v2.x, 0, v2.y));
                inputV3 = Vector3.ClampMagnitude(new Vector3(inputV3.x,0,inputV3.z).normalized * inputV3.magnitude, 1);
                v3Action(inputV3);
            });
        };
        walkAimAnimatorFunc = v2Action =>
            moveInput.Accept(v2 => v2Action(v2 * rigidbody.velocity.magnitude));
        runAnimatorFunc = fAction => fAction(rigidbody.velocity.magnitude);
        isAimingFunc = bAction => bAction(Input.GetMouseButton(1));
        runStartAction = () => humanAnimator.StartAnimation("Run").Play();
        runStateUpdateAction = () =>
        {
            moveRotationFunc(q => transform.rotation = q);
            moveVector3Func(v3 => rigidbody.velocity = v3 * runSpeed);
            runAnimatorFunc(f => animator.SetFloat("runBlend", f));
        };
        walkAimStartAction = () => humanAnimator.StartAnimation("WalkAim").Play();
        walkAimUpdateAction = () =>
        {
            moveAimRotationFunc(q => transform.rotation = q);
            moveVector3Func(v3 => rigidbody.velocity = v3 * walkSpeed);
            walkAimAnimatorFunc(v2 => 
            {
                animator.SetFloat("WalkAimX", v2.x);
                animator.SetFloat("WalkAimY", v2.y);
            });
        };

        runState = () => new HumanRunState(runStartAction, runStateUpdateAction, isAimingFunc, walkAimState);
        walkAimState = () => new HumanWalkAimState(walkAimStartAction, walkAimUpdateAction, isAimingFunc, runState);
        currentState = runState();
    }
    void Update()
    {
        if(currentState.NextState() != currentState)
        {
            currentState = currentState.NextState();
            currentState.Start();
        }
        currentState.Update();
    }
}
public delegate IHumanState HumanState();
