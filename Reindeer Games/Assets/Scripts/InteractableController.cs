using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;

public class InteractableController : MonoBehaviour
{
    private RoomController _room;
    private Vector3 _defaultScale;
    private Vector3 _hoverScale;
    [field: SerializeField]
    private MouseEvents _mouseEvents;
    [field: SerializeField]
    public string Name { get; private set; }
    public List<CursorDialogue> Dialogues;
    public List<CursorEntry> Interactions;
    public List<VerbOverride> VerbOverrides;
    

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
        if (EventSystem.current.IsPointerOverGameObject()) { return; }
        // transform.localScale = _hoverScale;
        _room.Interactable = this;
    }

    public void HandleMouseExit()
    {
        // transform.localScale = _defaultScale;
        if (_room.Interactable == this) { _room.Interactable = null; }
    }

    public void HandleClick()
    {
        if (EventSystem.current.IsPointerOverGameObject()) { return; }
        if (!_room.InteractionText.IsClear) 
        {
            _room.InteractionText.Clear();
            return;
        }
        PerformClick(_room.Cursor);
    }

    public void PerformClick(CursorData cursor)
    {
        foreach (CursorDialogue dialogue in Dialogues.Where(c => cursor.CurrentAction == c.CursorAction))
        {
            _room.InteractionText.Text = dialogue.Dialogue;
        }
        foreach (CursorEntry entry in Interactions.Where(c => cursor.CurrentAction == c.CursorAction))
        {
            entry.Event.Invoke();
        }
    }
}

[Serializable]
public class CursorDialogue
{
    [FormerlySerializedAs("Cursor")]
    public CursorActionData CursorAction;
    [TextArea(1, 5)]
    public string Dialogue;
}


[Serializable]
public class CursorEntry
{
    [FormerlySerializedAs("Cursor")]
    public CursorActionData CursorAction;
    public UnityEvent Event;
}