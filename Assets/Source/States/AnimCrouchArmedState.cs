public class AnimCrouchArmedState : IHumanState
{
    IAnimator animator;
    FloatFunc blend;
    IHumanStatus status;
    HumanState animRunState;
    HumanState animCrouchAimState;
    HumanState animCrouchState;
    public AnimCrouchArmedState(IAnimator animator, FloatFunc blend, IHumanStatus status, HumanState animRunState, HumanState animCrouchAimState, HumanState animCrouchState)
    {
        this.animator = animator;
        this.blend = blend;
        this.status = status;
        this.animRunState = animRunState;
        this.animCrouchAimState = animCrouchAimState;
        this.animCrouchState = animCrouchState;
    }
    public void Enter()
    {
        animator.StartAnimation("CrouchArmed").Play();
    }
    public void Update()
    {
        blend(f => animator.ParameterFloat("blend").Load(f));
    }
    public void Exit(){}
    public IHumanState NextState()
    {
        IHumanState next = this;;
        status.IsCrouching()(b => next = b? next : animRunState());
        status.IsArmed()(armed =>
        {
            status.IsAiming()(aiming => next = aiming && armed ? animCrouchAimState() : armed ? next : animCrouchState());
        });
        return next;
    }
}