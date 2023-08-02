using UnityEngine;
public class RotMoveState : IHumanState
{
    IVector3 moveVector;
    Transform human;
    IHumanStatus status;
    HumanState rotAimState;
    
    IRotation moveRotation;
    public RotMoveState(IVector3 moveVector, Transform human, IHumanStatus status, HumanState rotAimState)
    {
        this.moveVector = moveVector;
        this.human = human;
        this.status = status;
        this.rotAimState = rotAimState;
        moveRotation = new DirectionRotation(moveVector);
    }

    public void Enter(){}
    public void Update()
    {
        moveRotation.Accept(q => human.rotation = q);
    }
    public void Exit(){}
    public IHumanState NextState()
    {
        IHumanState next = this;
        status.IsArmed()(armed =>
        {
            status.IsAiming()(aiming =>
            {
                next = armed && aiming ? rotAimState() : next;
            });
        });
        return next;
    }
}
