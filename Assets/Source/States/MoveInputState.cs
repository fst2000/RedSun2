using UnityEngine;
public class MoveInputState : IHumanState
{
    IVector3 moveV3;
    IMoveSystem moveSystem;

    public MoveInputState(IVector3 moveV3, IMoveSystem moveSystem)
    {
        this.moveV3 = moveV3;
        this.moveSystem = moveSystem;
    }

    public void Enter(){}
    public void Update()
    {
        moveSystem.Move(moveV3);
    }
    public void Exit(){}
    public IHumanState NextState()
    {
        return this;
    }
}