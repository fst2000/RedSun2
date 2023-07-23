public class DelegateVector2 : IVector2
{
    Vector2Delegate method;

    public DelegateVector2(Vector2Delegate method)
    {
        this.method = method;
    }

    public void GiveVector2(IVector2Consumer consumer)
    {
        method(consumer);
    }
}
public delegate void Vector2Delegate(IVector2Consumer consumer);