public class AnimCrouchState : IHumanState
{
    IAnimator animator;
    FloatFunc blend;
    IHumanStatus status;
    HumanState animRunState;
    HumanState animCrouchAimState;
    HumanState animCrouchArmedState;

    public AnimCrouchState(IAnimator animator, FloatFunc blend, IHumanStatus status, HumanState animRunState, HumanState animCrouchAimState, HumanState animCrouchArmedState)
    {
        this.animator = animator;
        this.blend = blend;
        this.status = status;
        this.animRunState = animRunState;
        this.animCrouchAimState = animCrouchAimState;
        this.animCrouchArmedState = animCrouchArmedState;
    }

    public void Enter()
    {
        status.IsArmed()(b => animator.StartAnimation(b? "CrouchArmed" : "Crouch").Play());
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
            next = armed? animCrouchArmedState() : next;
        });
        return next;
    }
}