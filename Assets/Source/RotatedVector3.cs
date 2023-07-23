using UnityEngine;
public class RotatedVector3 : IVector3
{
    IRotation rotation;
    IVector3 vector;
    public RotatedVector3(IRotation rotation, IVector3 vector)
    {
        this.rotation = rotation;
        this.vector = vector;
    }

    public void GiveVector3(IVector3Consumer consumer)
    {
        vector.GiveVector3(new DelegateVector3Consumer(v3 =>
        {
            rotation.GiveRotation(new DelegateRotationConsumer(q =>
            {
                consumer.Consume(q * v3);
            }));
        }));
    }
}