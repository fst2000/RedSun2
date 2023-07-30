using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerBehaviour : MonoBehaviour
{
    [SerializeField] new Transform camera;
    [SerializeField] Animator animator;
    [SerializeField] float runSpeed;
    [SerializeField] float crouchSpeed;
    new Rigidbody rigidbody;
    CapsuleCollider capsuleCollider;
   void Start()
   {
        rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
        rigidbody.mass = 60;
        capsuleCollider = gameObject.AddComponent<CapsuleCollider>();
        capsuleCollider.height = 1.8f;
        capsuleCollider.radius = 0.5f;
        capsuleCollider.center = new Vector3(0, 0.9f, 0);
   }
   void Update()
   {
        
   }
   void FixedUpdate()
   {

   }
   void LateUpdate()
   {

   }
    
}
public delegate IHumanState HumanState();
