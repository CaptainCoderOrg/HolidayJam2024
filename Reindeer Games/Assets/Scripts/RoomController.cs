using System;
using Unity.VisualScripting;
using UnityEngine;

public class RoomController : MonoBehaviour 
{
    [field: SerializeField]
    public CursorDatabase Cursor { get; private set; }
    [SerializeField]
    private ScreenTextData _actionText;
    private InteractableController _interactable;
    public InteractableController Iteractabe 
    {
        get => _interactable;
        set
        {
            _interactable = value;
            UpdateActionText(Cursor.Current);
        }
    }

    void OnEnable()
    {
        Cursor.OnChange += UpdateActionText;
    }

    void OnDisable()
    {
        Cursor.OnChange -= UpdateActionText;
    }

    private void UpdateActionText(CursorData data)
    {
        _actionText.Text = $"{data.DefaultVerb} {_interactable?.Name}";
    }
}