using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class InteractableController : MonoBehaviour
{
    private RoomController _room;
    private Vector3 _defaultScale;
    private Vector3 _hoverScale;
    private MouseEvents _mouseEvents;
    [field: SerializeField]
    public string Name { get; private set; }
    public List<CursorEntry> Interactions;
    

    void Awake()
    {
        _room = GetComponentInParent<RoomController>();
        Debug.Assert(_room != null, "Interactable must be inside of a room");
        _defaultScale = transform.localScale;
        _hoverScale = _defaultScale + new Vector3(.1f, .1f, .1f);
        _mouseEvents = GetComponentInChildren<MouseEvents>();
        Debug.Assert(_mouseEvents != null);
    }

    void OnEnable()
    {
        _mouseEvents.OnEnter.AddListener(HandleMouseEnter);
        _mouseEvents.OnExit.AddListener(HandleMouseExit);
        _mouseEvents.OnClick.AddListener(HandleClick);
    }

    void OnDisable()
    {
        _mouseEvents.OnEnter.RemoveListener(HandleMouseEnter);
        _mouseEvents.OnExit.RemoveListener(HandleMouseExit);
        _mouseEvents.OnClick.RemoveListener(HandleClick); 
    }


    public void HandleMouseEnter()
    {
        transform.localScale = _hoverScale;
    }

    public void HandleMouseExit()
    {
        transform.localScale = _defaultScale;
    }

    public void HandleClick()
    {
        PerformClick(_room.Cursor.Current);
    }

    public void PerformClick(CursorData cursor)
    {
        foreach (CursorEntry entry in Interactions.Where(c => cursor == c.Cursor))
        {
            entry.Event.Invoke();
        }
    }
}

[Serializable]
public class CursorEntry
{
    public CursorData Cursor;
    public UnityEvent Event;
}