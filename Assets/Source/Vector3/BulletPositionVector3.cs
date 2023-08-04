using System;
using UnityEngine;
public class BulletPositionVector3 : IVector3
{
    Vector3 startPos;
    Vector3 direction;
    double startTime = Time.timeAsDouble;
    float speed;
    public BulletPositionVector3(Vector3 startPos, Vector3 direction, float speed)
    {
        this.startPos = startPos;
        this.direction = direction;
        this.speed = speed;
    }
    
    public void Accept(Action<Vector3> action)
    {
        double delta = Time.timeAsDouble - startTime;
        Vector3 position = startPos + (direction * (float)delta * speed);
        action(position);
    }
}