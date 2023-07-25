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
    IEvent update;
    Transform camera;
    new Rigidbody rigidbody;
    IHumanState currentState;
    HumanState runState;
    HumanState walkAimState;
    IAnimator humanAnimator;
    KeyboardMoveInput moveInput;
    RotationFunc runRotationFunc;
    Vector3Func runVector3Func;
    FloatFunc runAnimatorFunc;
    BoolFunc isAimingFunc;
    Action runStateUpdateAction;
    void Start()
    {
        update = new Event();
        camera = Camera.main.transform;

        rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.mass = 60;
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rigidbody.freezeRotation = true;

        humanAnimator = new UnityAnimator(animator);
        moveInput = new KeyboardMoveInput();
        runRotationFunc = qAction => runVector3Func(v3 => qAction(Quaternion.LookRotation(v3)));
        runVector3Func = v3Action =>
        {
            moveInput.Accept(v2 =>
            {
                Vector3 inputV3 = camera.TransformDirection(new Vector3(v2.x, 0, v2.y));
                inputV3 = new Vector3(inputV3.x,0,inputV3.z).normalized * inputV3.magnitude;
                v3Action(inputV3);
            });
        };
        runAnimatorFunc = fAction => fAction(rigidbody.velocity.magnitude);
        isAimingFunc = bAction => bAction(Input.GetMouseButton(1));
        runStateUpdateAction = () =>
        {
            runRotationFunc(q => transform.rotation = q);
            runVector3Func(v3 => rigidbody.velocity = v3 * runSpeed);
            runAnimatorFunc(f => animator.SetFloat("runBlend", f));
        };

        runState = () => new HumanRunState(runStateUpdateAction, update, isAimingFunc, humanAnimator, walkAimState);
        currentState = runState();
    }
    void Update()
    {
        if(currentState.NextState() != currentState) currentState = currentState.NextState();
        update.Call();
    }
}
public delegate IHumanState HumanState();
