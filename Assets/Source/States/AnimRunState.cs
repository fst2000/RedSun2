public class AnimRunState : IHumanState
{
    IAnimator animator;
    FloatFunc blend;
    IHumanStatus status;
    HumanState animCrouchState;
    HumanState animWalkAimState;
    HumanState animRunArmedState;

    public AnimRunState(IAnimator animator, FloatFunc blend, IHumanStatus status, HumanState animCrouchState, HumanState animWalkAimState, HumanState animRunArmedState)
    {
        this.animator = animator;
        this.blend = blend;
        this.status = status;
        this.animCrouchState = animCrouchState;
        this.animWalkAimState = animWalkAimState;
        this.animRunArmedState = animRunArmedState;
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
        status.IsCrouching()(b => next = b? animCrouchState() : next);
        status.IsArmed()(b => next = b? animRunArmedState() : next);
        return next;
    }
}