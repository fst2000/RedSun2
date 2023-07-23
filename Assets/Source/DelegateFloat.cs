public class DelegateFloat : IFloat
{
    FloatDelegate inputDelegate;

    public DelegateFloat(FloatDelegate inputDelegate)
    {
        this.inputDelegate = inputDelegate;
    }

    public void GiveFloat(IFloatConsumer consumer)
    {
        inputDelegate(consumer);
    }
}
public delegate void FloatDelegate(IFloatConsumer consumer);