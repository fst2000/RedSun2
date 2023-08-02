public class AnimRunArmedState : IHumanState
{
    IAnimator animator;
    FloatFunc blend;
    IHumanStatus status;
    HumanState animCrouchArmedState;
    HumanState animWalkAimState;
    HumanState animRunState;

    public AnimRunArmedState(IAnimator animator, FloatFunc blend, IHumanStatus status, HumanState animCrouchArmedState, HumanState animWalkAimState, HumanState animRunState)
    {
        this.animator = animator;
        this.blend = blend;
        this.status = status;
        this.animCrouchArmedState = animCrouchArmedState;
        this.animWalkAimState = animWalkAimState;
        this.animRunState = animRunState;
    }

    public void Enter()
    {
        animator.StartAnimation("RunArmed").Play();
    }
    public void Update()
    {
        blend(f => animator.ParameterFloat("blend").Load(f));
    }
    public void Exit(){}
    public IHumanState NextState()
    {
        IHumanState next = this;;
        status.IsCrouching()(b => next = b? animCrouchArmedState() : next);
        status.IsAiming()(b => next = b? animWalkAimState() : next);
        status.IsArmed()(b => next = b? next : animRunState());
        return next;
    }
}