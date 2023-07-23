using UnityEngine;
public class BoneIKMoveSystem : IVector3Consumer
{
    IEvent lateUpdateEvent;
    Transform bone1;
    Transform bone2;
    Transform boneIK;
    Transform target;
    float maxLength;
    Vector3 boneIKPosition = Vector3.zero;
    public BoneIKMoveSystem(IEvent lateUpdateEvent, Transform bone1, Transform bone2, Transform boneIK, Transform target, float maxLength)
    {
        lateUpdateEvent.Subscribe(LateUpdate);
        this.bone1 = bone1;
        this.bone2 = bone2;
        this.boneIK = boneIK;
        this.target = target;
        this.maxLength = maxLength;
    }

    public void Consume(Vector3 vector)
    {
        Vector3 localPosition = bone1.InverseTransformPoint(vector);
        if(localPosition.magnitude > maxLength)
        {
            localPosition = localPosition.normalized * maxLength;
        }
        boneIKPosition = bone1.TransformPoint(localPosition);
    }
    void LateUpdate()
    {
        boneIK.position = boneIKPosition;
    }
}