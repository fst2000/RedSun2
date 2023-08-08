using UnityEngine;
public class AutomatBullet : IBullet
{
    BulletPositionVector3 bulletPosition;
    Transform transform;
    GameObject bulletGameObject;
    BulletHit bulletHit;
    HitParticle hitParticle;
    HitNormalVector3 hitNormalVector3;
    public AutomatBullet(BulletPositionVector3 bulletPosition, IEvent update)
    {
        this.bulletPosition = bulletPosition;
        bulletHit = new BulletHit(bulletPosition);
        bulletPosition.Accept(pos =>
        {
            bulletGameObject = GameObject.Instantiate(Resources.Load<GameObject>("Weapon/AutomatBullet"), pos, Quaternion.identity);
        });
        transform = bulletGameObject.transform;
        update.Subscribe(Update);
        hitNormalVector3 = new HitNormalVector3(bulletHit);
        hitParticle = new HitParticle(bulletPosition, hitNormalVector3);
        
    }
    void Update()
    {
        bulletPosition.Accept(v3 =>
        {
            if(bulletGameObject != null) transform.position = v3;
        });
        bulletHit.Accept(hit =>
        {
            hitParticle.Show();
            GameObject.Destroy(bulletGameObject);
        });
    }
}