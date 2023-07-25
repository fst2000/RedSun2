public class HumanWalkArmedState : IHumanState
{
    IRotation moveRotation;
    IRotation aimRotation;
    IVector3 walkArmedVector3;
    IVector3Consumer moveConsumer;
    IRotationConsumer moveRotationConsumer;
    IRotationConsumer aimRotationConsumer;
    IFloatConsumer animatorFloatConsumer;
    public HumanWalkArmedState(
        HumanStateDelegate runState,
        IRotation moveRotation,
        IRotation aimRotation,
        IVector3 walkArmedVector3,
        IVector3Consumer moveConsumer,
        IRotationConsumer moveRotationConsumer,
        IRotationConsumer aimRotationConsumer,
        IAnimator animator,
        IEvent update,
        IEvent fixedUpdate)
    {
        this.moveRotation = moveRotation;
        this.aimRotation = aimRotation;
        this.walkArmedVector3 = walkArmedVector3;
        this.moveConsumer = moveConsumer;
        this.moveRotationConsumer = moveRotationConsumer;
        this.aimRotationConsumer = aimRotationConsumer;
        animator.StartAnimation("WalkArmed");
        update.Subscribe(Update);
        fixedUpdate.Subscribe(FixedUpdate);
    }
    void Update()
    {
        walkArmedVector3.GiveVector3(moveConsumer);
        moveRotation.GiveRotation(moveRotationConsumer);
        aimRotation.GiveRotation(aimRotationConsumer);
    }
    void FixedUpdate()
    {

    }
    public IHumanState NextState()
    {
        throw new System.NotImplementedException();
    }
}