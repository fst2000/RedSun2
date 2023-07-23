public class DelegareBoolConsumer : IBoolConsumer
{
    BoolConsumerDelegate consumerDelegate;

    public DelegareBoolConsumer(BoolConsumerDelegate consumerDelegate)
    {
        this.consumerDelegate = consumerDelegate;
    }

    public void Consume(bool input)
    {
        consumerDelegate(input);
    }
}
public delegate void BoolConsumerDelegate(bool input);