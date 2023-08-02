public class SizeIdleState : IHumanState
{
    IHumanSize size;
    IHumanStatus status;
    HumanState sizeCrouchState;
    public SizeIdleState(IHumanSize size, IHumanStatus status, HumanState sizeCrouchState)
    {
        this.size = size;
        this.status = status;
        this.sizeCrouchState = sizeCrouchState;
    }
    public void Enter() => size.Accept(1.8f);
    public void Update(){}
    public void Exit(){}
    public IHumanState NextState()
    {
        IHumanState next = this;
        status.IsCrouching()(b => next = b? sizeCrouchState() : next);
        return next;
    }
}