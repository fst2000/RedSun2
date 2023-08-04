using UnityEngine;
public class AutomatBullet : IBullet
{
    BulletPositionVector3 bulletPosition;
    Transform transform;
    public AutomatBullet(BulletPositionVector3 bulletPosition, IEvent update)
    {
        this.bulletPosition = bulletPosition;
        GameObject bulletGameObject = Resources.Load<GameObject>("Weapon/AutomatBullet");
        transform = bulletGameObject.transform;
        update.Subscribe(Update);
    }
    void Update()
    {
        bulletPosition.Accept(v3 => transform.position = v3);
    }
    public IMark Impact()
    {
        throw new System.NotImplementedException();
    }
}