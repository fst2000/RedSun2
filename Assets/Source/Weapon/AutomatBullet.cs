using UnityEngine;
public class AutomatBullet : IBullet
{
    BulletPositionVector3 bulletPosition;
    Transform transform;
    GameObject bulletGameObject;
    public AutomatBullet(BulletPositionVector3 bulletPosition, IEvent update)
    {
        this.bulletPosition = bulletPosition;
        bulletPosition.Accept(pos =>
        {
            bulletGameObject = GameObject.Instantiate(Resources.Load<GameObject>("Weapon/AutomatBullet"), pos, Quaternion.identity);
        });
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