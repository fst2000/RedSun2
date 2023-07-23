using UnityEngine;
public class CameraRotationVector2 : IVector2
{
    IVector2 input;
    Vector2 rotation = Vector2.zero;
    public CameraRotationVector2(IVector2 input)
    {
        this.input = input;
    }
    public void GiveVector2(IVector2Consumer consumer)
    {
        input.GiveVector2(new DelegateVector2Consumer(v2 =>
        {
            if(rotation.y >= 60) rotation.y = 60;
            if(rotation.y <= -60) rotation.y = -60;
            rotation += v2;
        }));
        Vector2 clampedRotation = new Vector2(rotation.x, Mathf.Clamp(rotation.y,-60,60));
        consumer.Consume(clampedRotation);
    }
}