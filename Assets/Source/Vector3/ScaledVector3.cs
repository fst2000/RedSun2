using System;
using UnityEngine;

public class ScaledVector3 : IVector3
{
    IVector3 vector3;
    float scale;
    public ScaledVector3(IVector3 vector3, float scale)
    {
        this.vector3 = vector3;
        this.scale = scale;
    }
    public void Accept(Action<Vector3> action)
    {
        vector3.Accept(v3 =>
        {
            action(v3 * scale);
        });
    }
}