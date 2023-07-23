using UnityEngine;

public class DelegateVector2Consumer : IVector2Consumer
{
    Vector2ConsumerDelegate consumerDelegate;

    public DelegateVector2Consumer(Vector2ConsumerDelegate consumerDelegate)
    {
        this.consumerDelegate = consumerDelegate;
    }

    public void Consume(Vector2 input)
    {
        consumerDelegate(input);
    }
}
public delegate void Vector2ConsumerDelegate(Vector2 input);