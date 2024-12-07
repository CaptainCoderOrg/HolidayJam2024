using System;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class RoomController : MonoBehaviour 
{
    [field: SerializeField]
    public CursorData Cursor { get; private set; }
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
            UpdateActionText(Cursor.CurrentAction);
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
        string proposition = _interactable == null ? string.Empty : data.DefaultProposition;
        if (_interactable?.VerbOverrides.FirstOrDefault(v => v.Cursor == data) is VerbOverride verbOverride)
        {
            verb = verbOverride.Verb;
        }
        _actionText.Text = ReplaceVariables($"{verb} {proposition} {_interactable?.Name}", Cursor).Replace("  ", " ");
    }

    private static string ReplaceVariables(string message, CursorData cursor)
    {
        if (!cursor.IsItemSelected) { return message; }
        const string ItemVerb = "{item-verb}";
        const string ItemName = "{item-name}";
        const string ItemProposition = "{item-proposition}";
        
        message = message.Replace(ItemVerb, cursor.SelectedItem.Verb)
                         .Replace(ItemName, cursor.SelectedItem.Name)
                         .Replace(ItemProposition, cursor.SelectedItem.Preposition); 
        return message;
    }
}