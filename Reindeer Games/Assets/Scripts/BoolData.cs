using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Game State/Bool")]
public class BoolData : Predicate
{
    [field: SerializeField]
    public string Description { get; private set; }
    [SerializeField]
    private bool _isComplete = false;
    public bool IsComplete 
    { 
        get => _isComplete;
        set
        {
            _isComplete = value;
            _onChange?.Invoke(this);
        } 
    }

    private bool _isCompleteInitial;

    public override bool IsMet(CursorData cursor) => IsComplete;

    private event System.Action<BoolData> _onChange;
    public event System.Action<BoolData> OnChange
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