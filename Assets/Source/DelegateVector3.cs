public class DelegateVector3 : IVector3
{
    Vector3Delegate action;
    public DelegateVector3(Vector3Delegate action)
    {
        this.action = action;
    }

    public void GiveVector3(IVector3Consumer vector3Consumer)
    {
        action(vector3Consumer);
    }
}
public delegate void Vector3Delegate(IVector3Consumer consumer);