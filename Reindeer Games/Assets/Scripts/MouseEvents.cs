using UnityEngine;
using UnityEngine.Events;

public class MouseEvents : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;

    void OnMouseEnter() => OnEnter.Invoke();
    void OnMouseExit() => OnExit.Invoke();
}
