using System;
public class HumanRunState : IHumanState
{
    Action updateAction;
    BoolFunc isAiming;

    public HumanRunState(Action updateAction, IEvent updateEvent, BoolFunc isAiming, IAnimator animator, HumanState walkAimState)
    {
        animator.StartAnimation("RunBlend");
        updateEvent.Subscribe(Update);
        this.updateAction = updateAction;
        this.isAiming = isAiming;
        this.walkAimState = walkAimState;
    }

    HumanState walkAimState;
    public IHumanState NextState()
    {
        bool isAimingBool = false;
        isAiming(b =>
        {
            isAimingBool = b;
        });
        if(isAimingBool) return walkAimState();
        else return this;
    }
    void Update()
    {
        updateAction();
    }
}