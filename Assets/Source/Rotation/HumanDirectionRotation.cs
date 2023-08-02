using System;
using UnityEngine;

public class DirectionRotation : IRotation
{
    IVector3 direction;
    public DirectionRotation(IVector3 direction)
    {
        this.direction = direction;
    }
    public void Accept(Action<Quaternion> action)
    {
        direction.Accept(v3 =>
        {
            if(v3 != Vector3.zero)
            {
                action(Quaternion.LookRotation(v3));
            }
        });
    }
}
