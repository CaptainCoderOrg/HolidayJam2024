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
        CursorDialogue dialogue = Dialogues.FirstOrDefault(c => cursor.CurrentAction == c.CursorAction);
        if (dialogue != null)
        {
            _room.InteractionText.Text = dialogue.Dialogue;
        }
        //  ?? cursor.CurrentAction.DefaultMessages.GetRandom();
        // _room.InteractionText.Text = message;
        int count = 0;
        foreach (CursorEntry entry in Interactions.Where(c => cursor.CurrentAction == c.CursorAction))
        {
            entry.Event.Invoke();
            count++;
        }

        if (dialogue == null && count == 0)
        {
            _room.InteractionText.Text = cursor.CurrentAction.DefaultMessages.GetRandom();
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