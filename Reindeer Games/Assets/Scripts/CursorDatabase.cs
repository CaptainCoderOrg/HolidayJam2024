#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CursorDatabase : ScriptableObject
{
    private event System.Action<CursorData> _onChange;
    public event System.Action<CursorData> OnChange
    {
        add
        {
            _onChange += value;
            value.Invoke(Current);
        }
        remove
        {
            _onChange -= value;
        }
    }

    private int _ix = 0;
    public CursorData Current => _cursors[_ix];
    [SerializeField]
    private List<CursorData> _cursors;

    public CursorData Next()
    {
        _ix = (_ix + 1) % _cursors.Count;
        _onChange?.Invoke(Current);
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