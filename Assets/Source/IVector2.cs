using System;
using UnityEngine;
public interface IVector2
{
    void Accept(Action<Vector2> action);
}