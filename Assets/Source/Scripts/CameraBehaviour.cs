using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CameraBehaviour : MonoBehaviour
{
    [SerializeField] Transform origin;
    [SerializeField] float rotationSpeed;
    [SerializeField] Vector3 defaultOffset;
    [SerializeField] Vector3 aimOffset;
    [SerializeField] float fovDef;
    [SerializeField] float fovAim;
    [SerializeField] float camSmooth;
    CameraRotationVector2 cameraRotationVector2;
    Vector3OffsetFunc cameraPosFunc;
    FloatFunc cameraFov;
    BoolFunc aimInput;
    void Start()
    {
        cameraRotationVector2 = new CameraRotationVector2(v2Action =>
        {
            v2Action(new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y")) * rotationSpeed);
        }, 60);
        cameraPosFunc = (action, offset) =>
        {
            Vector3 rotatedOffset = transform.rotation * new Vector3(offset.x,0,offset.z);
            action(origin.position + new Vector3(0, offset.y, 0) + rotatedOffset);
        };
        cameraFov = fAction => aimInput(b => fAction(b? fovAim : fovDef));
        aimInput = bAction => bAction(Input.GetMouseButton(1));
    }
    void Update()
    {
        cameraRotationVector2.Accept(v2 =>
            transform.rotation = Quaternion.Euler(new Vector3(-v2.y, v2.x, 0)));
        aimInput(b =>
        {
            cameraPosFunc(v3 => transform.position = v3,  b? aimOffset : defaultOffset);
        });
        cameraFov(f =>
        {
            float fov = Camera.main.fieldOfView;
            fov = Mathf.Lerp(fov, f, Time.deltaTime * camSmooth);
            Camera.main.fieldOfView = fov;
        });
    }
    delegate void Vector3OffsetFunc(Action<Vector3> action, Vector3 offset);
}
