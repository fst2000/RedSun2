using UnityEngine;
public class WalkVector3 : IVector3
{
    IFloat walkSpeed;
    IVector3 vector;
    public WalkVector3(IFloat walkSpeed, IVector3 vector)
    {
        this.walkSpeed = walkSpeed;
        this.vector = vector;
    }
    public void GiveVector3(IVector3Consumer consumer)
    {
        vector.GiveVector3(new DelegateVector3Consumer(v =>
        {
            walkSpeed.GiveFloat(new DelegateFloatConsumer(f =>
            {
                consumer.Consume(new Vector3(v.x, 0, v.z) * f);
            }));
        }));
    }
}