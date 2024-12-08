using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryController : MonoBehaviour
{
    [field: SerializeField]
    public InventoryData Inventory { get; private set; }

    private InventoryButton[] _inventoryButtons;
    [field: SerializeField]
    public UnityEvent<InventoryItemData> OnItemSelected { get; private set;}

    void Awake()
    {
        _inventoryButtons = GetComponentsInChildren<InventoryButton>();
        foreach (var button in _inventoryButtons)
        {
            button.OnItemSelected.AddListener(item => OnItemSelected.Invoke(item));
        }
    }

    void OnEnable()
    {
        Inventory.OnItemsModified += UpdateButtons;
    }

    void OnDisable()
    {
        Inventory.OnItemsModified -= UpdateButtons;
    }

    private void UpdateButtons(List<InventoryItemData> items)
    {
        for (int ix = 0; ix < _inventoryButtons.Length; ix++)
        {
            InventoryButton iButton = _inventoryButtons[ix];
            iButton.Item = items.GetAtOrDefault(ix);
        }
    }

}