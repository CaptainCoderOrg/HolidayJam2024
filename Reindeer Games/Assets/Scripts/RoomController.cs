using System;
using UnityEngine;

public class RoomController : MonoBehaviour 
{
    [field: SerializeField]
    public CursorDatabase Cursor { get; private set; }
    [SerializeField]
    private ScreenTextData _actionText;

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
        _actionText.Text = data.DefaultVerb;
    }
}