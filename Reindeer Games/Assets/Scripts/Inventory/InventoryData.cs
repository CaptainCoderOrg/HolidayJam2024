using System.Collections.Generic;
using UnityEngine;
using System.Linq;


#if UNITY_EDITOR
using UnityEditor;
#endif

[CreateAssetMenu(menuName = "Inventory/Inventory")]
public class InventoryData : ScriptableObject
{
    [field: SerializeField]
    public List<InventoryItemData> StartingItems { get; private set; }
    [field: SerializeField]
    public List<InventoryItemData> Items { get; private set; }
    private event System.Action<List<InventoryItemData>> _onItemsModified;
    public event System.Action<List<InventoryItemData>> OnItemsModified
    {
        add
        {
            _onItemsModified += value;
            value.Invoke(Items);
        }
        remove
        {
            _onItemsModified -= value;
        }
    }

    public void AddItem(InventoryItemData item)
    {
        Items.Add(item);
        _onItemsModified.Invoke(Items);
    }

    public void RemoveItem(InventoryItemData item)
    {
        if(Items.Remove(item))
        {
            _onItemsModified.Invoke(Items);
        }
    }

#if UNITY_EDITOR
    void OnEnable()
    {
        EditorApplication.playModeStateChanged += OnPlayModeStateChange;
    }

    void OnDisable()
    {
        EditorApplication.playModeStateChanged -= OnPlayModeStateChange;
    }

    private void OnPlayModeStateChange(PlayModeStateChange change)
    {
        if (change is PlayModeStateChange.EnteredPlayMode)
        {
            Items = StartingItems.ToList();
            _onItemsModified.Invoke(Items);
        }
        if (change is PlayModeStateChange.ExitingPlayMode)
        {
            _onItemsModified = null;
            Items.Clear();
        }
    }
#endif

}