using UnityEngine;
public class WalkVector3 : IVector3
{
    float walkSpeed;
    IVector3 vector;
    public WalkVector3(float walkSpeed, IVector3 vector)
    {
        this.walkSpeed = walkSpeed;
        this.vector = vector;
    }
    public void GiveVector3(IVector3Consumer consumer)
    {
        vector.GiveVector3(new DelegateVector3Consumer(v =>
        {
            float length = v.magnitude;
            consumer.Consume(new Vector3(v.x, 0, v.z).normalized * length * walkSpeed);
        }));
    }
}