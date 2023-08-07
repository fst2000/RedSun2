using UnityEngine;
using System;
public interface IHit
{
    void Accept(Action<RaycastHit> action);
}