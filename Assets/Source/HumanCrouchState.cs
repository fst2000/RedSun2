using System;
public class HumanCrouchState : IHumanState
{
    IAnimator animator;
    FloatFunc crouchBlend;
    IHumanSize humanSize;
    IHumanStatus humanStatus;
    HumanState runState;

    float crouchSpeed = 3f;
    public HumanCrouchState(IAnimator animator, FloatFunc crouchBlend, IHumanSize humanSize, IHumanStatus humanStatus, HumanState runState)
    {
        this.animator = animator;
        this.crouchBlend = crouchBlend;
        this.humanSize = humanSize;
        this.humanStatus = humanStatus;
        this.runState = runState;
    }
    public void Enter()
    {
        humanStatus.isArmed()(b => animator.StartAnimation(b?"CrouchArmed" : "Crouch").Play());
        humanSize.Accept(1.2f);
    }
    public void Update()
    {
        crouchBlend(f => animator.ParameterFloat("crouch").Load(f));
    }
    public void Exit()
    {

    }

    public IHumanState NextState()
    {
        IHumanState nextState = this;
        humanStatus.isCrouching()(b => nextState = b? runState() : this);
        return nextState;
    }
}