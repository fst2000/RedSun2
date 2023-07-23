using UnityEngine;
public class FootPositionVector3 : IVector3
{
    Transform human;
    Vector3 footPivot;
    Vector3 footPosition;
    Vector3 targetPosition;
    bool isFirst;
    public FootPositionVector3(Transform human, Vector3 footPivot, bool isFirst)
    {
        this.human = human;
        this.footPivot = footPivot;
        this.isFirst = isFirst;
        footPosition = human.TransformPoint(footPivot);
        targetPosition = footPosition;
    }
    public void GiveVector3(IVector3Consumer consumer)
    {
        Vector3 footPivotGlobal = human.TransformPoint(footPivot);
        Vector3 stepDelta = footPosition - footPivotGlobal;

        Vector3 rayPosition = footPivotGlobal + human.up;
        Ray ray = new Ray(rayPosition, -human.up);
        RaycastHit hit;
        if(Physics.Raycast(ray, out hit) && hit.distance < 1.1f)
        {
            targetPosition.y = footPivotGlobal.y + Mathf.Max(0, human.up.y - hit.distance);
            footPosition = targetPosition;
        }
        else footPosition = footPivotGlobal;
        consumer.Consume(footPosition);
    }
}