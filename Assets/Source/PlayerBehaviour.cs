using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] float walkSpeed;

    IEvent update = new Event();
    IEvent fixedUpdate = new Event();

    new Rigidbody rigidbody;
    Animator animator;
    new Transform camera;

    IHumanState currentState;
    HumanStateDelegate runState;
    HumanStateDelegate walkArmedState;
    IRotation moveRotation;
    IRotation aimRotation;
    IRotation moveArmedRotation;
    IVector2 moveInputVector2;
    IVector3 runVector3;
    IFloat runFloat;
    IRotationConsumer transformRotationConsumer;
    void Start()
    {
        rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
        rigidbody.mass = 60;
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        
        animator = gameObject.GetComponent<Animator>();

        camera = Camera.main.transform;

        moveInputVector2 = new DelegateVector2(v2Consumer =>
            v2Consumer.Consume(
                    new Vector2(
                        Input.GetAxis("Horizontal"),
                        Input.GetAxis("Vertical"))
            )
        );
        runVector3 = new WalkVector3(5, new ClampedVector3(1, 
            new DelegateVector3(consumer =>
            {
                moveInputVector2.GiveVector2(new DelegateVector2Consumer(v2 =>
                {
                    consumer.Consume(camera.TransformDirection(new Vector3(v2.x,0,v2.y)));
                }));
            }
        )
        ));
        runFloat = new DelegateFloat(consumer =>
        {
            moveInputVector2.GiveVector2(new DelegateVector2Consumer(v2 =>
            {
                consumer.Consume(v2.magnitude);
            }));
        });
        moveRotation = new DelegateRotation(consumer =>
        {
            runVector3.GiveVector3(new DelegateVector3Consumer(v3=>
            {
                if(v3.magnitude > 0.01f)
                {
                    consumer.Consume(Quaternion.Lerp(
                        transform.rotation,
                        Quaternion.LookRotation(v3, Vector3.up), Time.deltaTime * 5));
                }
            }));
        });
        aimRotation = new DelegateRotation(consumer =>
        {
            consumer.Consume(Quaternion.LookRotation(camera.transform.forward));
        });
        moveArmedRotation = new DelegateRotation(consumer =>
        {
            Vector3 moveDir = camera.transform.forward;
            moveDir.y = 0;
            consumer.Consume(Quaternion.LookRotation(moveDir));
        });
        transformRotationConsumer = new DelegateRotationConsumer(q =>
        {
            transform.rotation = q;
        });

        runState = () => new HumanRunState(
            walkArmedState,
            moveRotation,
            runVector3,
            new RigidbodyVelocityConsumer(fixedUpdate,rigidbody),
            transformRotationConsumer,
            new FloatBlendAnimator(animator, runFloat, "runBlend", update),
            update, fixedUpdate);
        walkArmedState = () => new HumanWalkArmedState();
        currentState = runState();
    }
    void Update()
    {
        if(currentState != currentState.NextState())
        {
            currentState = currentState.NextState();
        }
        update.Call();
    }
    void FixedUpdate()
    {
        fixedUpdate.Call();
    }
    void LateUpdate()
    {

    }
}
public delegate IHumanState HumanStateDelegate();
