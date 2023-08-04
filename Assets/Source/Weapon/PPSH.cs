using UnityEngine;
public class PPSH : IWeapon
{
    GameObject ppsh;
    Transform ppshTransform;
    Transform transform;
    Vector3 shootPoint = new Vector3(0, 0.04f, 0.6f);
    public PPSH()
    {
        ppsh = Resources.Load<GameObject>("PPSH");
        transform = ppsh.transform;
    }

    public IBullet Shoot()
    {
        return null;
    }
}