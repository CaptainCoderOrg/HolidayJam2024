using System;
using UnityEngine;
using UnityEngine.UI;

public class CursorItemController : MonoBehaviour
{
     
    [SerializeField]
    private Image _selectedImage;
    [SerializeField]
    private CursorData _cursor;

    void OnEnable()
    {
        _cursor.OnChange += HandleCursorChange;
        _cursor.OnSelectedItemChanged += UpdateImage;
    }

    void OnDisable()
    {
        _cursor.OnChange -= HandleCursorChange;
        _cursor.OnSelectedItemChanged -= UpdateImage;
    }

    private void HandleCursorChange(CursorActionData action) => _selectedImage.gameObject.SetActive(action == _cursor.ItemCursorAction);
    private void UpdateImage(InventoryItemData item) => _selectedImage.sprite = item?.Icon;

}