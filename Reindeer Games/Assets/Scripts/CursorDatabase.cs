using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CursorDatabase : ScriptableObject
{

    private int _ix = 0;
    public CursorData Current => _cursors[_ix];
    [SerializeField]
    private List<CursorData> _cursors;

    public CursorData Next()
    {
        _ix = (_ix + 1) % _cursors.Count;
        return _cursors[_ix];
    }

}