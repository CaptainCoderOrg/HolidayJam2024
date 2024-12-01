#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

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
            _text = value;
            _onChange?.Invoke(_text);
        }
    }

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