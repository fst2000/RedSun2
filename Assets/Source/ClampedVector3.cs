using UnityEngine;
public class ClampedVector3 : IVector3
{
    float maxMagnitude;
    IVector3 vector;

    public ClampedVector3(float maxMagnitude, IVector3 vector)
    {
        this.maxMagnitude = maxMagnitude;
        this.vector = vector;
    }

    public void GiveVector3(IVector3Consumer consumer)
    {
        vector.GiveVector3(new DelegateVector3Consumer(v3 =>
        {
            consumer.Consume(Vector3.ClampMagnitude(v3, maxMagnitude));
        }));
    }
}