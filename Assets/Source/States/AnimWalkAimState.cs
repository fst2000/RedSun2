public class AnimWalkAimState : IHumanState
{
    IAnimator animator;
    Vector2Func blendXY;
    IHumanStatus status;
    HumanState animCrouchAimState;
    HumanState animRunState;
    public AnimWalkAimState(IAnimator animator, Vector2Func blendXY, IHumanStatus status, HumanState animCrouchAimState, HumanState animRunState)
    {
        this.animator = animator;
        this.blendXY = blendXY;
        this.status = status;
        this.animCrouchAimState = animCrouchAimState;
        this.animRunState = animRunState;
    }
    public void Enter()
    {
        animator.StartAnimation("WalkAim").Play();
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
        status.IsArmed()(armed =>
        {
            status.IsAiming()(aiming => next = aiming && armed ? next : animRunState());
        });
        status.IsCrouching()(b => next = b? animCrouchAimState() : next);
        return next;
    }
}