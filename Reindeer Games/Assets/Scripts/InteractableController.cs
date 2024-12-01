using UnityEngine;

public class InteractableController : MonoBehaviour
{
    private Vector3 _defaultScale;
    private Vector3 _hoverScale;
    private MouseEvents _mouseEvents;
    

    void Awake()
    {
        _defaultScale = transform.localScale;
        _hoverScale = _defaultScale + new Vector3(.1f, .1f, .1f);
        _mouseEvents = GetComponentInChildren<MouseEvents>();
        Debug.Assert(_mouseEvents != null);
    }

    void OnEnable()
    {
        _mouseEvents.OnEnter.AddListener(HandleMouseEnter);
        _mouseEvents.OnExit.AddListener(HandleMouseExit);
    }

    void OnDisable()
    {
        _mouseEvents.OnEnter.RemoveListener(HandleMouseEnter);
        _mouseEvents.OnExit.RemoveListener(HandleMouseExit);
    }


    public void HandleMouseEnter()
    {
        transform.localScale = _hoverScale;
    }

    public void HandleMouseExit()
    {
        transform.localScale = _defaultScale;
    }

}
