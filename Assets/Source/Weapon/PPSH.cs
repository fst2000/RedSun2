using UnityEngine;
public class PPSH : IWeapon
{
    IVector3 position;
    IRotation rotation;
    IEvent update;
    GameObject ppsh;
    Transform ppshTransform;
    Transform transform;
    Vector3 shootPoint = new Vector3(0, 0.04f, 0.6f);
    float bulletSpeed = 100f;
    public PPSH(IVector3 position, IRotation rotation, IEvent update)
    {
        this.position = position;
        this.rotation = rotation;
        this.update = update;
        update.Subscribe(Update);
        ppsh = GameObject.Instantiate(Resources.Load<GameObject>("Weapon/PPSH"));
        transform = ppsh.transform;
    }
    void Update()
    {
        position.Accept(v3 => transform.position = v3);
        rotation.Accept(q => transform.rotation = q);
    }
    public void Shoot()
    {
        new AutomatBullet(new BulletPositionVector3(transform.TransformPoint(shootPoint), transform.forward, bulletSpeed), update);
    }

    public void Reload()
    {
        
    }
}