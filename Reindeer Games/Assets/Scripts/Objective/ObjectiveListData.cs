using System.Collections.Generic;
using UnityEngine;
using System.Linq;


#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Objectives/List")]
public class ObjectiveListData : ScriptableObject
{
    [field: SerializeField]
    public List<ObjectiveData> StartingObjectives { get; private set; } = new();

    [field: SerializeField]
    public List<ObjectiveData> Objectives { get; private set; } = new();

    private event System.Action<List<ObjectiveData>> _onChange;
    public event System.Action<List<ObjectiveData>> OnChange
    {
        add
        {
            _onChange += value;
            value.Invoke(Objectives);
        }
        remove
        {
            _onChange -= value;
        }
    }

    void OnEnable()
    {
        Objectives = StartingObjectives.ToList();
#if UNITY_EDITOR
        EditorEnable();
#endif
    }

    void OnDisable()
    {
#if UNITY_EDITOR
        EditorDisable();
#endif        
    }

#if UNITY_EDITOR

    private void EditorEnable()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChange;
    }

    private void EditorDisable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChange;
    }

    private void OnPlayModeStateChange(PlayModeStateChange change)
    {
        if (change is PlayModeStateChange.EnteredPlayMode)
        {
            Objectives = StartingObjectives.ToList();
            _onChange.Invoke(Objectives);
        }
        if (change is PlayModeStateChange.ExitingPlayMode)
        {
            _onChange = null;
            Objectives.Clear();
        }
    }
#endif

}