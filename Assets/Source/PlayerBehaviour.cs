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

    IHumanState currentState;
    
    void Start()
    {
        rigidbody = gameObject.AddComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
        rigidbody.mass = 60;
        rigidbody.collisionDetectionMode = CollisionDetectionMode.Continuous;
        
        animator = gameObject.GetComponent<Animator>();

        currentState = new HumanRunState(rigidbody,animator, update,fixedUpdate);
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
