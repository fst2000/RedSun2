using System;
public class HumanWalkAimState : IHumanState
{
    IAnimator animator;
    Vector2Func runBlend;
    IHumanSize humanSize;
    IHumanStatus humanStatus;
    HumanState crouchState;

    float runSpeed = 3f;
    public HumanWalkAimState(IAnimator animator, Vector2Func runBlend, IHumanSize humanSize, IHumanStatus humanStatus, HumanState crouchState)
    {
        this.animator = animator;
        this.runBlend = runBlend;
        this.humanSize = humanSize;
        this.humanStatus = humanStatus;
        this.crouchState = crouchState;
    }
    public void Enter()
    {
        animator.StartAnimation("WalkAim").Play();
        humanSize.Accept(1.8f);
    }
    public void Update()
    {
        runBlend(v2 =>
        {
            animator.ParameterFloat("walkAimX").Load(v2.x);
            animator.ParameterFloat("walkAimY").Load(v2.y);
        });
    }
    public void Exit()
    {

    }

    public IHumanState NextState()
    {
        IHumanState nextState;
        humanStatus.isCrouching()(b => nextState = b? crouchState() : this);
        return crouchState();
    }
}