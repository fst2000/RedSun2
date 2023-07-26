using UnityEngine;
using System;
public class HumanWalkAimState : IHumanState, ILateUpdatable
{
    Action startAction;
    Action updateAction;
    Action lateUpdateAction;
    BoolFunc isAiming;
    HumanState runState;
    public HumanWalkAimState(Action startAction, Action updateAction, Action lateUpdateAction, BoolFunc isAiming, HumanState runState)
    {
        this.startAction = startAction;
        this.updateAction = updateAction;
        this.isAiming = isAiming;
        this.runState = runState;
        this.lateUpdateAction = lateUpdateAction;
    }

    public void LateUpdate()
    {
        lateUpdateAction();
    }

    public IHumanState NextState()
    {
        IHumanState nextState = this;;
        isAiming(b =>
        {
            if(!b) nextState = runState();
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