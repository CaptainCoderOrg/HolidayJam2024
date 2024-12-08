using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Objectives/Objective")]
public class ObjectiveData : ScriptableObject
{
    [field: SerializeField]
    public string Description { get; private set; }
    [field: SerializeField]
    public bool IsComplete { get; set; }

    private bool _isCompleteInitial;


    private event System.Action<ObjectiveData> _onChange;
    public event System.Action<ObjectiveData> OnChange
    {
        add
        {
            _onChange += value;
            value.Invoke(this);
        }
        remove
        {
            _onChange -= value;
        }
    }

    void OnEnable()
    {
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
    void OnValidate()
    {
        _onChange?.Invoke(this);
    }

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
            _isCompleteInitial = IsComplete;
        }
        if (change is PlayModeStateChange.ExitingPlayMode)
        {
            IsComplete = _isCompleteInitial;
            _onChange = null;
        }
    }
#endif

}