using UnityEngine;

public class DelegateVector3Consumer : IVector3Consumer
{
    Vector3ConsumerDelegate consumerDelegate;

    public DelegateVector3Consumer(Vector3ConsumerDelegate consumerDelegate)
    {
        this.consumerDelegate = consumerDelegate;
    }

    public void Consume(Vector3 vector)
    {
        consumerDelegate(vector);
    }
}
public delegate void Vector3ConsumerDelegate(Vector3 vector);