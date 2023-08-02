public class AnimCrouchState : IHumanState
{
    IAnimator animator;
    FloatFunc blend;
    IHumanStatus status;
    HumanStates states;

    public AnimCrouchState(IAnimator animator, FloatFunc blend, IHumanStatus status, HumanStates states)
    {
        this.animator = animator;
        this.blend = blend;
        this.status = status;
        this.states = states;
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
        status.IsCrouching()(b => next = b? next : states.animRun());
        status.IsArmed()(armed =>
        {
            next = armed? states.animCrouchArmed() : next;
        });
        return next;
    }
}