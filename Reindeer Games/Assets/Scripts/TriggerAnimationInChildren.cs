using UnityEngine;

public class TriggerAnimationInChildren : MonoBehaviour
{
    public void SetAnimatorTriggerInChildren(string name)
    {
        var animators = GetComponentsInChildren<Animator>();
        foreach (var animator in animators)
        {
            animator.SetTrigger(name);
        }
    }
}
