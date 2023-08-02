using System;
using UnityEngine;
public class HumanStates
{

    IAnimator humanAnimator;
    IHumanStatus status;
    IMoveSystem moveSystem;
    IVector3 moveVector3;
    FloatFunc animBlend;
    Vector2Func animBlendXY;
    Transform transform;
    Transform camera;

    public HumanState animRun;
    public HumanState animCrouch;
    public HumanState animWalkAim;
    public HumanState animCrouchAim;
    public HumanState animRunArmed;
    public HumanState animCrouchArmed;
    public HumanState moveInput;
    public HumanState rotMove;
    public HumanState rotAim;
    public HumanStates(IAnimator humanAnimator, IHumanStatus status, IMoveSystem moveSystem, IVector3 moveVector3, FloatFunc animBlend, Vector2Func animBlendXY, Transform transform, Transform camera)
    {
        this.humanAnimator = humanAnimator;
        this.status = status;
        this.moveSystem = moveSystem;
        this.moveVector3 = moveVector3;
        this.animBlend = animBlend;
        this.animBlendXY = animBlendXY;
        this.transform = transform;
        this.camera = camera;

        animRun = () => new AnimRunState(humanAnimator, animBlend, status, this);
        animCrouch = () => new AnimCrouchState(humanAnimator, animBlend, status, this);
        animWalkAim = () => new AnimWalkAimState(humanAnimator, animBlendXY, status, this);
        animCrouchAim = () => new AnimCrouchAimState(humanAnimator, animBlendXY, status, this);
        animRunArmed = () => new AnimRunArmedState(humanAnimator, animBlend, status, this);
        animCrouchArmed = () => new AnimCrouchArmedState(humanAnimator, animBlend, status, this);

        rotMove = () => new RotMoveState(moveVector3, transform, status, rotAim);
        rotAim = () => new RotAimState(transform, camera, status, rotMove);

        moveInput = () => new MoveInputState(moveVector3, moveSystem);
    }
}