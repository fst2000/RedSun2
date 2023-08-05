using UnityEngine;
public class RotAimState : IHumanState
{
    Transform human;
    IHumanStatus status;
    IRotation aimRotation;
    HumanState rotMoveState;

    public RotAimState(Transform human, Transform target, IHumanStatus status, HumanState rotMoveState)
    {
        this.human = human;
        this.status = status;
        this.rotMoveState = rotMoveState;
        aimRotation = new LookRotation(new FlatVector3(new TransformForwardVector3(target)));
    }

    public void Enter(){}
    public void Update()
    {
        aimRotation.Accept(q => human.rotation = q);
    }
    public void Exit(){}
    public IHumanState NextState()
    {
        IHumanState next = this;
        status.IsArmed()(armed =>
        {
            status.IsAiming()(aiming =>
            {
                next = armed && aiming ? next : rotMoveState();
            });
        });
        return next;
    }
}
