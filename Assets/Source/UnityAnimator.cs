using UnityEngine;
public class UnityAnimator : IAnimator
{
    Animator animator;

    public UnityAnimator(Animator animator)
    {
        this.animator = animator;
    }

    public IParameter ParameterFloat(string name)
    {
        return null;
    }

    public IAnimation StartAnimation(string name)
    {
        throw new System.NotImplementedException();
    }
}