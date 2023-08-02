public class AnimCrouchArmedState : IHumanState
{
    IAnimator animator;
    FloatFunc blend;
    IHumanStatus status;
    HumanStates states;

    public AnimCrouchArmedState(IAnimator animator, FloatFunc blend, IHumanStatus status, HumanStates states)
    {
        this.animator = animator;
        this.blend = blend;
        this.status = status;
        this.states = states;
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
        status.IsCrouching()(b => next = b? next : states.animRun());
        status.IsArmed()(armed =>
        {
            status.IsAiming()(aiming => next = aiming && armed ? states.animCrouchAim() : armed ? next : states.animCrouch());
        });
        return next;
    }
}