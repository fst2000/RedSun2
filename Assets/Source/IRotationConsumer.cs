using UnityEngine;
public interface IRotationConsumer
{
    void Consume(Quaternion rotation);
}