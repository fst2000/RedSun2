using UnityEngine;
public class FloatBlendAnimator : IAnimator
{
    Animator animator;
    IFloat blend;
    string floatName;
    IEvent update;
    public FloatBlendAnimator(Animator animator, IFloat blend, string floatName, IEvent update)
    {
        this.animator = animator;
        this.blend = blend;
        this.floatName = floatName;
        update.Subscribe(Update);
    }
    public void StartAnimation(string name)
    {
        animator.CrossFade(name, 0.1f);
    }
    public void Update()
    {
         blend.GiveFloat(new DelegateFloatConsumer(f =>
         {
            animator.SetFloat(floatName, f);
         }));
    }
}