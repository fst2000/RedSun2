using UnityEngine;
public class ColliderHumanSize : IHumanSize
{
    CapsuleCollider collider;
    float size;
    public ColliderHumanSize(CapsuleCollider collider)
    {
        this.collider = collider;
    }

    public void Accept(float size)
    {
        if(this.size != size)
        {
            collider.height = size;
            collider.center = new Vector3(0, size * 0.5f, 0);
            this.size = size;
        }
    }
}