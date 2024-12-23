
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu]
public class ScreenTextData : ScriptableObject
{
    [field: SerializeField]
    private string _text;
    public string Text
    {
        get => _text;
        set
        {
            _text = value.Trim();
            _onChange?.Invoke(_text);
        }
    }

    public void ShowDialogueText(DialogueTextData dialogueText) => Text = dialogueText.Message;

    private event System.Action<string> _onChange;
    public event System.Action<string> OnChange
    {
        add
        {
            _onChange += value;
            value.Invoke(Text);
        }
        remove
        {
            _onChange -= value;
        }
    }

    public void Clear()
    {
        Text = string.Empty;
    }

    public bool IsClear => Text == null || Text.Trim().Length == 0;

#if UNITY_EDITOR
    void OnValidate()
    {
        _onChange?.Invoke(Text);
    }
    private void OnEnable()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChanged;
    }

    private void OnDisable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChanged;
    }

    private void OnPlayModeStateChanged(PlayModeStateChange state)
    {
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            _onChange = null;
        }
    }
#endif
}