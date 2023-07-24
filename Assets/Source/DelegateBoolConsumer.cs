public class DelegateBoolConsumer : IBoolConsumer
{
    BoolConsumerDelegate consumerDelegate;

    public DelegateBoolConsumer(BoolConsumerDelegate consumerDelegate)
    {
        this.consumerDelegate = consumerDelegate;
    }

    public void Consume(bool input)
    {
        consumerDelegate(input);
    }
}
public delegate void BoolConsumerDelegate(bool input);