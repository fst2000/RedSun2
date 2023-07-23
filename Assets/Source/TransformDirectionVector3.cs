using UnityEngine;
public class TransformDirectionVector3 : IVector3
{
    Transform transform;
    IVector3 vector;

    public TransformDirectionVector3(Transform transform, IVector3 vector)
    {
        this.transform = transform;
        this.vector = vector;
    }

    public void GiveVector3(IVector3Consumer consumer)
    {
        vector.GiveVector3(new DelegateVector3Consumer(v3 =>
        {
            consumer.Consume(transform.TransformDirection(v3));
        }));
    }
}