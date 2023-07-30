using System;
public class HumanRunState : IHumanState
{
    IAnimator animator;
    FloatFunc runBlend;
    IHumanSize humanSize;
    IHumanStatus humanStatus;
    HumanState crouchState;

    float runSpeed = 5f;
    public HumanRunState(IAnimator animator, FloatFunc runBlend, IHumanSize humanSize, IHumanStatus humanStatus, HumanState crouchState)
    {
        this.animator = animator;
        this.runBlend = runBlend;
        this.humanSize = humanSize;
        this.humanStatus = humanStatus;
        this.crouchState = crouchState;
    }
    public void Enter()
    {
        humanStatus.isArmed()(b => animator.StartAnimation(b?"RunArmed" : "Run").Play());
        humanSize.Accept(1.8f);
    }
    public void Update()
    {
        runBlend(f => animator.ParameterFloat("run").Load(f));
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