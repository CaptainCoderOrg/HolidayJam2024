#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CursorData : ScriptableObject
{
    private event System.Action<CursorActionData> _onChange;
    public event System.Action<CursorActionData> OnChange
    {
        add
        {
            _onChange += value;
            value.Invoke(CurrentAction);
        }
        remove
        {
            _onChange -= value;
        }
    }

    private int _ix = 0;
    public CursorActionData CurrentAction => _cursors[_ix];
    [SerializeField]
    private List<CursorActionData> _cursors;

    public CursorActionData Next()
    {
        _ix = (_ix + 1) % _cursors.Count;
        _onChange?.Invoke(CurrentAction);
        return _cursors[_ix];
    }

    

#if UNITY_EDITOR
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