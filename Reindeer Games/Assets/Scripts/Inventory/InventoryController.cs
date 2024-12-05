using System.Collections.Generic;
using UnityEngine;

public class InventoryController : MonoBehaviour
{
    [field: SerializeField]
    public InventoryData Inventory { get; private set; }

    private InventoryButton[] _inventoryButtons;

    void Awake()
    {
        _inventoryButtons = GetComponentsInChildren<InventoryButton>();
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