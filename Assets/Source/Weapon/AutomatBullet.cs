using UnityEngine;
public class AutomatBullet : IBullet
{
    BulletPositionVector3 bulletPosition;
    Transform transform;
    GameObject bulletGameObject;
    BulletHit bulletHit;
    Vector3 startPoint;
    public AutomatBullet(BulletPositionVector3 bulletPosition, IEvent update)
    {
        this.bulletPosition = bulletPosition;
        bulletHit = new BulletHit(bulletPosition);
        bulletPosition.Accept(pos =>
        {
            bulletGameObject = GameObject.Instantiate(Resources.Load<GameObject>("Weapon/AutomatBullet"), pos, Quaternion.identity);
            startPoint = pos;
        });
        transform = bulletGameObject.transform;
        update.Subscribe(Update);
    }
    void Update()
    {
        bulletPosition.Accept(v3 =>
        {
            transform.position = v3;
        });
    }
}