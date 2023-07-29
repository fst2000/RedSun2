using UnityEngine;
public class BonesIK
{
    Transform bone1;
    Transform bone2;
    Transform bone3;
    Transform target;

    public BonesIK(Transform bone1, Transform bone2, Transform bone3, Transform target)
    {
        this.bone1 = bone1;
        this.bone2 = bone2;
        this.bone3 = bone3;
        this.target = target;
    }

    public void LateUpdate()
    {
        Vector3 bone1Pos = bone1.position;
        Vector3 bone2Pos = bone2.position;
        Vector3 bone3Pos = bone3.position;
        Vector3 targetPos = target.position;

        Vector3 targetDirection = targetPos - bone1Pos;
        Vector3 limbDirection = bone3Pos - bone1Pos;
        float bone1Length = (bone1Pos - bone2Pos).magnitude;
        float bone2Length = (bone2Pos - bone3Pos).magnitude;
        float targetDistance = (targetDirection).magnitude;
        bone3.position = targetPos;
        bone2.rotation = Quaternion.LookRotation(targetPos - bone2Pos);
        bone1.rotation = Quaternion.FromToRotation(limbDirection, targetDirection) * bone1.rotation;
    }
}
