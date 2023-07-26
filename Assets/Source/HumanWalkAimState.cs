using UnityEngine;
using System;
public class HumanWalkAimState : IHumanState
{
    Action startAction;
    Action updateAction;
    BoolFunc isAiming;
    HumanState runState;
    public HumanWalkAimState(Action startAction, Action updateAction, BoolFunc isAiming, HumanState runState)
    {
        this.startAction = startAction;
        this.updateAction = updateAction;
        this.isAiming = isAiming;
        this.runState = runState;
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