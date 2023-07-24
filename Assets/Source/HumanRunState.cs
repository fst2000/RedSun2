using UnityEngine;
public class HumanRunState : IHumanState
{
    IRotation rotation;
    IVector3 runVector3;
    IVector3Consumer moveConsumer;
    IRotationConsumer rotationConsumer;
    IFloatConsumer animatorFloatConsumer;

    public HumanRunState(
        IRotation rotation,
        IVector3 runVector3,
        IVector3Consumer moveConsumer,
        IRotationConsumer rotationConsumer,
        IAnimator animator,
        IEvent update,
        IEvent fixedUpdate)
    {
        this.runVector3 = runVector3;
        this.moveConsumer = moveConsumer;
        this.rotationConsumer = rotationConsumer;
        animator.StartAnimation("Run");
        this.rotation = rotation;
        update.Subscribe(Update);
        fixedUpdate.Subscribe(FixedUpdate);
    }

    void Update()
    {
        runVector3.GiveVector3(moveConsumer);
        rotation.GiveRotation(rotationConsumer);
    }
    void FixedUpdate()
    {

    }
    public IHumanState NextState()
    {
        return this;
    }
}