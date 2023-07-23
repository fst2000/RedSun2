using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] Transform origin;
    [SerializeField] Vector3 offset;
    [SerializeField] float rotationSpeed;

    IEvent lateUpdate = new Event();

    IRotation cameraRotation;
    IVector3 cameraPosition;
    IVector2 mouseInput;

    IRotationConsumer cameraRotationConsumer;
    TransformPositionVector3Consumer cameraPositionConsumer;
    void Start()
    {
        cameraRotation = new CameraRotation(
            new CameraRotationVector2(
                new DelegateVector2(v2Consumer =>
                    mouseInput.GiveVector2(
                        new DelegateVector2Consumer(v2 =>
                            v2Consumer.Consume(v2 * rotationSpeed)
                        )
                    )
                )
            ),
            new DelegateRotation(c => c.Consume(Quaternion.Euler(0,0,0)))
        );
        cameraPosition = new SumVector3(
            new DelegateVector3(consumer =>
                consumer.Consume(origin.position + new Vector3(0, offset.y, 0))
            ),
            new RotatedVector3(cameraRotation,
                new DelegateVector3(consumer =>
                {
                    consumer.Consume(new Vector3(offset.x,0,offset.z));
                })));
        mouseInput = new DelegateVector2(consumer =>
        {
            consumer.Consume(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")));
        });

        cameraRotationConsumer = new DelegateRotationConsumer(q =>
        {
            transform.rotation = q;
        });
        cameraPositionConsumer = new TransformPositionVector3Consumer(transform);
    }
    void Update()
    {
        cameraRotation.GiveRotation(cameraRotationConsumer);
        cameraPosition.GiveVector3(cameraPositionConsumer);
    }
    void LateUpdate()
    {
        lateUpdate.Call();
    }
}
