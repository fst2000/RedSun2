using System;
using UnityEngine;
public class HumanRunState : IHumanState
{
    Vector3Func moveDirFunc;
    Action<string> animStart;
    Action<string, float> animBlend;
    Action<Quaternion> rotation;
    Action<Vector3> velocity;

    string moveAnim = "Run";
    float moveSpeed = 5;
    RotationFunc moveRotFunc;

    public HumanRunState(Vector3Func moveDirFunc, Action<string> animStart, Action<string, float> animBlend, Action<Quaternion> rotation, Action<Vector3> velocity)
    {
        this.moveDirFunc = moveDirFunc;
        this.animStart = animStart;
        this.animBlend = animBlend;
        this.rotation = rotation;
        this.velocity = velocity;

        moveRotFunc = qAction => qAction(Quaternion.LookRotation(Vector3.forward));
    }

    public void Start()
    {
        animStart(moveAnim);
    }
    public void Update()
    {
        
        moveDirFunc(v3 =>
        {
            if(v3.magnitude > 0.01f) moveRotFunc = qAction => qAction(Quaternion.LookRotation(v3));
            velocity(v3 * moveSpeed);
            animBlend("runBlend", v3.magnitude);
        });
        moveRotFunc(q =>
        {
            rotation(q);
        });
    }
    public void Exit()
    {
        
    }

    public IHumanState NextState()
    {
        return this;
    }
}