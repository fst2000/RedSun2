public class AnimRunArmedState : IHumanState
{
    IAnimator animator;
    FloatFunc blend;
    IHumanStatus status;
    HumanStates states;

    public AnimRunArmedState(IAnimator animator, FloatFunc blend, IHumanStatus status, HumanStates states)
    {
        this.animator = animator;
        this.blend = blend;
        this.status = status;
        this.states = states;
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
        status.IsCrouching()(b => next = b? states.animCrouchArmed() : next);
        status.IsAiming()(b => next = b? states.animWalkAim() : next);
        status.IsArmed()(b => next = b? next : states.animRun());
        return next;
    }
}