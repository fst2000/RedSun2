public class DelegateFloatConsumer : IFloatConsumer
{
    FloatConsumerDelegate consumerDelegate;
    public DelegateFloatConsumer(FloatConsumerDelegate consumerDelegate)
    {
        this.consumerDelegate = consumerDelegate;
    }

    public void Consume(float value)
    {
        consumerDelegate(value);
    }
}
public delegate void FloatConsumerDelegate(float value);