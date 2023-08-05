using System;
using UnityEngine;
public class WeaponPositionVector3 : IVector3
{
    Transform spine2;
    IRotation aimRotation;
    IVector3 offset;
    public WeaponPositionVector3(Transform spine2, IRotation aimRotation, IVector3 offset)
    {
        this.spine2 = spine2;
        this.aimRotation = aimRotation;
        this.offset = offset;
    }

    public void Accept(Action<Vector3> action)
    {
        aimRotation.Accept(q =>
        {
            offset.Accept(v3 =>
            {
                action(spine2.position + q * v3);
            });
        });
    }
}