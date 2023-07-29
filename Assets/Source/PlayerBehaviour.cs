using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] new Transform camera;
    [SerializeField] Animator animator;
    new Rigidbody rigidbody;
    Action<string> animStart;
    Action<string, float> animBlend;
    Action<string, string, float, float> animBlendXY;
    Action<Vector3> velocity;
    Action<Quaternion> rotation;
    Func<Vector3, Quaternion> lookRot;
    Vector3Func moveInput;
    Vector3Func moveVector3;
    RotationFunc tRotation;
    RotationFunc camMoveRotFunc;
    HumanState runState;
    IHumanState currentState;
   void Start()
   {
        rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
        rigidbody.mass = 60;
        animStart = name => animator.CrossFade(name, 0.25f);
        animBlend = (name, f) => animator.SetFloat(name, f);
        animBlendXY = (nameX, nameY, x, y) =>
        {
            animator.SetFloat(nameX,x);
            animator.SetFloat(nameY, y);
        };
        velocity = v3 => rigidbody.velocity = new Vector3(v3.x, rigidbody.velocity.y + v3.y, v3.z);
        rotation = q => transform.rotation = q;
        lookRot = v3 => Quaternion.LookRotation(v3);
        moveInput = v3Action =>
        v3Action(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")));
        moveVector3 = v3Action =>
        {
            moveInput(v3 =>
            {
                Vector3 tDir = camera.TransformDirection(v3);
                tDir = Vector3.ClampMagnitude(new Vector3(tDir.x,0,tDir.z).normalized * tDir.magnitude, 1);
                v3Action(tDir);
            });
        };
        tRotation = qAction => qAction(transform.rotation);
        camMoveRotFunc = qAction =>
        {
            Vector3 cDir = camera.transform.forward;
            qAction(Quaternion.LookRotation(new Vector3(cDir.x,0,cDir.z)));
        };

        runState = () => new HumanRunState(moveVector3, animStart, animBlend, rotation, velocity);
        currentState = runState();
        currentState.Start();
   }
   void Update()
   {
        if(currentState != currentState.NextState())
        {
            currentState.Exit();
            currentState = currentState.NextState();
            currentState.Start();
        }
        currentState.Update();
   }
   void FixedUpdate()
   {

   }
   void LateUpdate()
   {

   }
    
}
public delegate IHumanState HumanState();
