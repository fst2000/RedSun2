using System;
using UnityEngine;

public class WeaponOffsetVector3 : IVector3
{
    Vector3 walkOffset;
    Vector3 crouchOffset;
    IHumanStatus status;
    public WeaponOffsetVector3(Vector3 walkOffset, Vector3 crouchOffset, IHumanStatus status)
    {
        this.walkOffset = walkOffset;
        this.crouchOffset = crouchOffset;
        this.status = status;
    }
    public void Accept(Action<Vector3> action)
    {
        status.IsCrouching()(crouching =>
        {
            action(crouching ? crouchOffset : walkOffset);
        });
    }
}