using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryButton : MonoBehaviour
{
    [field: SerializeField]
    public Image Image { get; private set; }
    [field: SerializeField]
    public CursorData Cursor { get; private set; }
    private InventoryItemData _item;
    public InventoryItemData Item 
    { 
        get => _item;
        set
        {
            _item = value;
            if (_item == null)
            {
                Image.gameObject.SetActive(false);
            }
            else
            {
                Image.gameObject.SetActive(true);
                Image.sprite = _item.Icon;
            }
        }
    }

    public void OnClick()
    {
        Cursor.SelectedItem = _item;
    }

    public void ClearItem()
    {
        Item = null;
    }


}