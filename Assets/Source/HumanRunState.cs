using System;
public class HumanRunState : IHumanState
{
    IVector3 moveDirection;
    IMoveSystem moveSystem;
    IAnimator animator;

    float runSpeed = 5f;
    public HumanRunState(IVector3 moveDirection, IMoveSystem moveSystem, IAnimator animator)
    {
        this.moveDirection = moveDirection;
        this.moveSystem = moveSystem;
        this.animator = animator;
    }
    public void Enter()
    {
        animator.StartAnimation("Run").Play();
        moveSystem.Move(new ScaledVector3(moveDirection, runSpeed));
    }
    public void Update()
    {
        moveDirection.Accept(v3=>
        {
            animator.ParameterFloat("runBlend").Load(v3.magnitude);
        });
    }
    public void Exit()
    {
        throw new System.NotImplementedException();
    }

    public IHumanState NextState()
    {
        throw new System.NotImplementedException();
    }
}