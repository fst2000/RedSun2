using UnityEngine;
public class AnimatorFloatConsumer : IFloatConsumer
{
    Animator animator;
    string floatName;
    public AnimatorFloatConsumer(Animator animator, string floatName)
    {
        this.animator = animator;
        this.floatName = floatName;
    }
    public void Consume(float value)
    {
        animator.SetFloat(floatName, value);
    }
}