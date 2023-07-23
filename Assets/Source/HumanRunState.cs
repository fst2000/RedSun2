using UnityEngine;
public class HumanRunState : IHumanState
{
    IRotation rigidbodyRotation;
    IVector2 moveInput;
    IVector3 runVector;
    IFloat walkSpeedInput;
    IFloat animatorRunBlend;

    AnimatorFloatConsumer animatorRunBlendConsumer;
    RigidbodyVelocityConsumer rigidbodyVelocityConsumer;
    IRotationConsumer transformRotationConsumer;

    float walkSpeed = 5f;
    public HumanRunState(Rigidbody rigidbody, Animator animator, IEvent update, IEvent fixedUpdate)
    {
        Transform transform = rigidbody.transform;
        update.Subscribe(Update);
        fixedUpdate.Subscribe(FixedUpdate);
        rigidbodyRotation = new DelegateRotation(c =>
        {
            Vector3 direction = rigidbody.velocity;
            direction.y = 0;
            if(direction.magnitude > 0.1)
                c.Consume(Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * 5));
        }
        );
        moveInput = new DelegateVector2(
            v2Consumer => v2Consumer.Consume(
                    new Vector2(
                        Input.GetAxis("Horizontal"),
                        Input.GetAxis("Vertical"))
                    )
                );
        walkSpeedInput = new DelegateFloat(
            fConsumer => fConsumer.Consume(walkSpeed)
        );
        runVector = new WalkVector3(
            walkSpeedInput,
            new ClampedVector3(1,
                new TransformDirectionVector3(
                    Camera.main.transform,
                    new DelegateVector3(v3Consumer =>
                        moveInput.GiveVector2(
                            new DelegateVector2Consumer(v2 =>
                            v3Consumer.Consume(new Vector3(v2.x, 0, v2.y))
                            )
                        )
                    )
                )    
            )
        );
        animatorRunBlend = new DelegateFloat(fConsumer =>
        {
            fConsumer.Consume(rigidbody.velocity.magnitude / walkSpeed);
        });

        animatorRunBlendConsumer = new AnimatorFloatConsumer(animator, "runBlend");
        rigidbodyVelocityConsumer = new RigidbodyVelocityConsumer(fixedUpdate, rigidbody);
        transformRotationConsumer = new DelegateRotationConsumer(q =>
            transform.rotation = q
        );
    }
    void Update()
    {
        runVector.GiveVector3(rigidbodyVelocityConsumer);
        animatorRunBlend.GiveFloat(animatorRunBlendConsumer);
        rigidbodyRotation.GiveRotation(transformRotationConsumer);
    }
    void FixedUpdate()
    {

    }
    public IHumanState NextState()
    {
        return this;
    }
}