#if UNITY_EDITOR
using UnityEditor;
#endif

using System.Collections.Generic;
using UnityEngine;
using Unity.VisualScripting;

[CreateAssetMenu]
public class CursorData : ScriptableObject
{
    [field: SerializeField]
    public CursorActionData ItemCursorAction { get; private set; }
    [field: SerializeField]
    public InventoryData Inventory { get; private set; }
    
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

    private event System.Action<InventoryItemData> _onSelectedItemChanged;
    public event System.Action<InventoryItemData> OnSelectedItemChanged
    {
        add
        {
            _onSelectedItemChanged += value;
            value.Invoke(SelectedItem);
        }
        remove
        {
            _onSelectedItemChanged -= value;
        }
    }

    private int _ix = 0;
    public CursorActionData CurrentAction => _cursors[_ix];
    [SerializeField]
    private List<CursorActionData> _cursors;
    [SerializeField]
    private InventoryItemData _selectedItem;
    public InventoryItemData SelectedItem 
    { 
        get => _selectedItem;
        set
        {
            _selectedItem = value;
            _onSelectedItemChanged?.Invoke(_selectedItem);
            if (_selectedItem == null)
            {
                Next();
            }
            else
            {
                SetCursorAction(ItemCursorAction);
            }
        }
    }
    public bool IsItemSelected => SelectedItem != null;

    public void DestroySelected()
    {
        if (!IsItemSelected) 
        { 
            Debug.LogError($"Attempted to Destroy Selected but no item was selected");
            return;
        }
        Inventory.RemoveItem(SelectedItem);
        SelectedItem = null;
    }

    public CursorActionData SetCursorAction(CursorActionData cursorAction)
    {
        int foundIx = _cursors.IndexOf(cursorAction);
        if (foundIx < 0) { return CurrentAction; }
        if (!IsItemSelected && cursorAction == ItemCursorAction) { return Next(); }
        _ix = foundIx;
        _onChange?.Invoke(CurrentAction);
        return _cursors[_ix];
    }

    public CursorActionData Next()
    {
        _ix = (_ix + 1) % _cursors.Count;
        if (!IsItemSelected && CurrentAction == ItemCursorAction) { return Next(); }
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
        if (state == PlayModeStateChange.EnteredPlayMode)
        {
            SelectedItem = null;
        }
        if (state == PlayModeStateChange.ExitingPlayMode)
        {
            SelectedItem = null;
            _onChange = null;
            _onSelectedItemChanged = null;
        }
    }
#endif

}