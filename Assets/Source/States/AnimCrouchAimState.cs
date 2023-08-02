public class AnimCrouchAimState : IHumanState
{
    IAnimator animator;
    Vector2Func blendXY;
    IHumanStatus status;
    HumanStates states;

    public AnimCrouchAimState(IAnimator animator, Vector2Func blendXY, IHumanStatus status, HumanStates states)
    {
        this.animator = animator;
        this.blendXY = blendXY;
        this.status = status;
        this.states = states;
    }

    public void Enter()
    {
        animator.StartAnimation("CrouchAim").Play();
    }
    public void Update()
    {
        blendXY(v2 => 
        {
            animator.ParameterFloat("blendX").Load(v2.x);
            animator.ParameterFloat("blendY").Load(v2.y);
        });
    }
    public void Exit(){}
    public IHumanState NextState()
    {
        IHumanState next = this;;
        status.IsCrouching()(b => next = b? next : states.animWalkAim());
        status.IsArmed()(armed =>
        {
            status.IsAiming()(aiming => next = aiming && armed ? next : states.animCrouch());
        });
        return next;
    }
}