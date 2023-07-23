using UnityEngine;
public class CameraRotation : IRotation
{
    IVector2 vector2;
    IRotation rotation;
    public CameraRotation(IVector2 vector2, IRotation rotation)
    {
        this.vector2 = vector2;
        this.rotation = rotation;
    }
    public void GiveRotation(IRotationConsumer consumer)
    {
        rotation.GiveRotation(new DelegateRotationConsumer(rotation =>
        {
            vector2.GiveVector2(new DelegateVector2Consumer(v2 =>
            {
                consumer.Consume(Quaternion.Euler(-v2.y,v2.x,0));
            }));
        }));   
    }
}