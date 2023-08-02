public class AnimCrouchAimState : IHumanState
{
    IAnimator animator;
    Vector2Func blendXY;
    IHumanStatus status;
    HumanState animWalkAimState;
    HumanState animCrouchState;
    public AnimCrouchAimState(IAnimator animator, Vector2Func blendXY, IHumanStatus status, HumanState animWalkAimState, HumanState animCrouchState)
    {
        this.animator = animator;
        this.blendXY = blendXY;
        this.status = status;
        this.animWalkAimState = animWalkAimState;
        this.animCrouchState = animCrouchState;
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
        status.IsCrouching()(b => next = b? next : animWalkAimState());
        status.IsArmed()(armed =>
        {
            status.IsAiming()(aiming => next = aiming && armed ? next : animCrouchState());
        });
        return next;
    }
}