using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : MonoBehaviour 
{
    public UnityEvent OnTrigger;

    public void Trigger() => OnTrigger.Invoke();
}