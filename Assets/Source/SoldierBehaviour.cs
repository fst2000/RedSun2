using System;
using UnityEngine;

public class SoldierBehaviour : MonoBehaviour
{
    [SerializeField] Animator animator;
    SoldierState state;

    void Start()
    {
        state = SoldierCommonEnterState.Walk(new InputVector2().Accept, animationName => () => animator.Play(animationName));
    }

    void Update()
    {
        state((movement, rotation, next) =>
        {
            transform.position = movement(transform.position);
            transform.rotation = rotation(transform.rotation);
            state = next;
        });
    }
}

public delegate void Animation();

public delegate Animation AnimatorFunc(string name);

public delegate void StateApply(Func<Vector3, Vector3> movement, Func<Quaternion, Quaternion> rotation, SoldierState next);

public delegate void SoldierState(StateApply apply);

public delegate void Vector3Func(Action<float, float, float> action);

public delegate void Vector2Func(Action<float, float> action);

public class InputVector2
{
    public void Accept(Action<float, float> action) => action(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
}

public class SoldierCommonEnterState
{
    AnimatorFunc animator;
    Animation idle;
    Animation walk;
    float speed;
    Vector2Func input;
    Func<Vector2Func, AnimatorFunc, SoldierState> next;

    public static SoldierState Crouch(Vector2Func input, AnimatorFunc animator) => new SoldierCommonEnterState("CrouchIdle", "CrouchWalk", 3f, animator, input, Walk).Apply;
    public static SoldierState Walk(Vector2Func input, AnimatorFunc animator) => new SoldierCommonEnterState("Idle", "Walk", 5f, animator, input, Crouch).Apply;

    public SoldierCommonEnterState(string idle, string walk, float speed, AnimatorFunc animator, Vector2Func input, Func<Vector2Func, AnimatorFunc, SoldierState> next)
    {
        this.animator = animator;
        this.idle = animator(idle);
        this.walk = animator(walk);
        this.speed = speed;
        this.next = next;
        this.input = input;
    }

    void SoldierUpdateState(StateApply apply)
    {
        if(Input.GetKeyDown(KeyCode.LeftShift))
        {
            apply(v => v, q => q, next(input, animator));
        }
        else
        {
            input((x, y) =>
            {
                apply(v => v + new Vector3(x, 0f, y) * Time.deltaTime * 3f,
                    q => q, SoldierUpdateState);
                if(x == 0f && y == 0f) idle();
                else walk();
            });
        }
    }

    public void Apply(StateApply apply)
    {
        Vector2Func input = new InputVector2().Accept;
        apply(v => v, q => q, SoldierUpdateState);
    }
}