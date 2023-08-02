public class SizeCrouchState : IHumanState
{
    IHumanSize size;
    IHumanStatus status;
    HumanState sizeIdleState;
    public SizeCrouchState(IHumanSize size, IHumanStatus status, HumanState sizeIdleState)
    {
        this.size = size;
        this.status = status;
        this.sizeIdleState = sizeIdleState;
    }
    public void Enter() => size.Accept(1.2f);
    public void Update(){}
    public void Exit(){}
    public IHumanState NextState()
    {
        IHumanState next = this;
        status.IsCrouching()(b => next = b? next : sizeIdleState());
        return next;
    }
}