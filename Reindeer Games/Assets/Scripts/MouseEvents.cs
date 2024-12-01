using UnityEngine;
using UnityEngine.Events;

public class MouseEvents : MonoBehaviour
{
    public UnityEvent OnEnter;
    public UnityEvent OnExit;
    public UnityEvent OnClick;

    void OnMouseEnter() => OnEnter.Invoke();
    void OnMouseExit() => OnExit.Invoke();
    void OnMouseUpAsButton() => OnClick.Invoke();
}
