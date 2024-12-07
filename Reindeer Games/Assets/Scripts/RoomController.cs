using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RoomController : MonoBehaviour 
{
    [field: SerializeField]
    public CursorDatabase Cursor { get; private set; }
    [SerializeField]
    private ScreenTextData _actionText;
    [field: SerializeField]
    public ScreenTextData InteractionText { get; private set; }
    private InteractableController _interactable;
    public InteractableController Interactable 
    {
        get => _interactable;
        set
        {
            _interactable = value;
            UpdateActionText(Cursor.Current);
        }
    }

    void Awake()
    {
        InteractionText.Text = string.Empty;
    }

    void OnEnable()
    {
        Cursor.OnChange += UpdateActionText;
    }

    void OnDisable()
    {
        Cursor.OnChange -= UpdateActionText;
    }

    private void UpdateActionText(CursorActionData data)
    {
        string verb = data.DefaultVerb;
        if (_interactable?.VerbOverrides.FirstOrDefault(v => v.Cursor == data) is VerbOverride verbOverride)
        {
            verb = verbOverride.Verb;
        }
        _actionText.Text = $"{verb} {_interactable?.Name}";
    }
}