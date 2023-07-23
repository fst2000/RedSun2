using UnityEngine;
public class DelegateRotation : IRotation
{
    RotationDelegate method;

    public DelegateRotation(RotationDelegate method)
    {
        this.method = method;
    }

    public void GiveRotation(IRotationConsumer consumer)
    {
        method(consumer);
    }
}
public delegate void RotationDelegate(IRotationConsumer consumer);