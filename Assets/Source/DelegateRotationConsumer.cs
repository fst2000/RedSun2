using UnityEngine;
public class DelegateRotationConsumer : IRotationConsumer
{
    RotationConsumerDelegate method;

    public DelegateRotationConsumer(RotationConsumerDelegate method)
    {
        this.method = method;
    }

    public void Consume(Quaternion rotation)
    {
        method(rotation);
    }
}
public delegate void RotationConsumerDelegate(Quaternion rotation);