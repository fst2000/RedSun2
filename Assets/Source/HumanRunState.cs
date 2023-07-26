using System;
public class HumanRunState : IHumanState
{
    Action startAction;
    Action updateAction;
    BoolFunc isAiming;
    HumanState walkAimState;

    public HumanRunState(Action startAction, Action updateAction, BoolFunc isAiming, HumanState walkAimState)
    {
        this.startAction = startAction;
        this.updateAction = updateAction;
        this.isAiming = isAiming;
        this.walkAimState = walkAimState;
    }
    public IHumanState NextState()
    {
        IHumanState nextState = this;;
        isAiming(b =>
        {
            if(b) nextState = walkAimState();
        });
        return nextState;
    }

    public void Start()
    {
        startAction();
    }

    public void Update()
    {
        updateAction();
    }
}