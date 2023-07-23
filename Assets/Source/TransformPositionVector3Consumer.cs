using UnityEngine;
public class TransformPositionVector3Consumer : IVector3Consumer
{
    Transform transform;
    public TransformPositionVector3Consumer(Transform transform)
    {
        this.transform = transform;
    }
    public void Consume(Vector3 position)
    {
        transform.position = position;
    }
}