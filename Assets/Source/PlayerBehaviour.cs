using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float walkSpeed;

    IEvent fixedUpdate = new Event();

    new Rigidbody rigidbody;
    Animator animator;

    IRotation rigidbodyRotation;
    IVector2 moveInput;
    IVector3 moveVector;
    IFloat walkSpeedInput;
    IFloat animatorRunBlend;
    
    AnimatorFloatConsumer animatorRunBlendConsumer;
    RigidbodyVelocityConsumer rigidbodyVelocityConsumer;
    IRotationConsumer transformRotationConsumer;
    void Start()
    {
        rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
        rigidbody.mass = 60;
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        rigidbodyVelocityConsumer = new RigidbodyVelocityConsumer(fixedUpdate, rigidbody);
        
        animator = gameObject.GetComponent<Animator>();

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
        moveVector = new WalkVector3(
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
        transformRotationConsumer = new DelegateRotationConsumer(q =>
            transform.rotation = q
        );
    }
    void Update()
    {
        moveVector.GiveVector3(rigidbodyVelocityConsumer);
        animatorRunBlend.GiveFloat(animatorRunBlendConsumer);
        rigidbodyRotation.GiveRotation(transformRotationConsumer);
    }
    void FixedUpdate()
    {
        fixedUpdate.Call();
    }
    void LateUpdate()
    {

    }
}
