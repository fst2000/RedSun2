public class AnimRunState : IHumanState
{
    IAnimator animator;
    FloatFunc blend;
    IHumanStatus status;
    HumanStates states;

    public AnimRunState(IAnimator animator, FloatFunc blend, IHumanStatus status, HumanStates states)
    {
        this.animator = animator;
        this.blend = blend;
        this.status = status;
        this.states = states;
    }

    public void Enter()
    {
        animator.StartAnimation("Run").Play();
    }
    public void Update()
    {
        blend(f => animator.ParameterFloat("blend").Load(f));
    }
    public void Exit(){}
    public IHumanState NextState()
    {
        IHumanState next = this;;
        status.IsCrouching()(b => next = b? states.animCrouch() : next);
        status.IsArmed()(b => next = b? states.animRunArmed() : next);
        return next;
    }
}