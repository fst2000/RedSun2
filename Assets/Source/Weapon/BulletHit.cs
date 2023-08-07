using System;
using UnityEngine;

public class BulletHit : IHit
{
    IVector3 bulletPosition;
    Vector3 rayStart;
    public BulletHit(IVector3 bulletPosition)
    {
        this.bulletPosition = bulletPosition;
        bulletPosition.Accept(point => rayStart = point);
    }
    public void Accept(Action<RaycastHit> action)
    {
        RaycastHit hit;
        bulletPosition.Accept(point =>
        {
            Vector3 direction = point - rayStart;
            Ray ray = new Ray(point, direction);
            rayStart = point;
            if(Physics.Raycast(ray, out hit) && hit.distance <= direction.magnitude) action(hit);
        });
    }
}