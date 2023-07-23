using UnityEngine;
public class RigidbodyVelocityConsumer : IVector3Consumer
{
    IEvent fixedUpdate;
    Rigidbody rigidbody;
    Vector3 velocity = Vector3.zero;

    public RigidbodyVelocityConsumer(IEvent fixedUpdate, Rigidbody rigidbody)
    {
        fixedUpdate.Subscribe(FixedUpdate);
        this.rigidbody = rigidbody;
    }
    void FixedUpdate()
    {
        float gravity = rigidbody.velocity.y;
        rigidbody.velocity = new Vector3(velocity.x, gravity + velocity.y, velocity.z);
    }
    public void Consume(Vector3 vector)
    {
        velocity = vector;
    }
}