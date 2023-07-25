using UnityEngine;
public class HumanRunState : IHumanState
{
    IRotation rotation;
    IVector3 runVector3;
    IVector3Consumer moveConsumer;
    IRotationConsumer rotationConsumer;
    IFloatConsumer animatorFloatConsumer;

    public HumanRunState(
        HumanStateDelegate walkArmedState,
        IRotation rotation,
        IVector3 runVector3,
        IVector3Consumer moveConsumer,
        IRotationConsumer rotationConsumer,
        IAnimator animator,
        IEvent update)
    {
        this.runVector3 = runVector3;
        this.moveConsumer = moveConsumer;
        this.rotationConsumer = rotationConsumer;
        animator.StartAnimation("Run");
        this.rotation = rotation;
        update.Subscribe(Update);
    }

    void Update()
    {
        runVector3.GiveVector3(moveConsumer);
        rotation.GiveRotation(rotationConsumer);
    }
    public IHumanState NextState()
    {
        return this;
    }
}