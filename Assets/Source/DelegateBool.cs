public class DelegateBool : IBool
{
    BoolDelegate inputDelegate;

    public DelegateBool(BoolDelegate inputDelegate)
    {
        this.inputDelegate = inputDelegate;
    }

    public void GiveInput(IBoolConsumer consumer)
    {
        inputDelegate(consumer);
    }
}
public delegate void BoolDelegate(IBoolConsumer consumer);