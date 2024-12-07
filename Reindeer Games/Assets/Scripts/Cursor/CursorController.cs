using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour 
{
    private RoomController _room;
    [SerializeField]
    private Image _selectedImage;

    void Awake()
    {
        _room = GetComponentInParent<RoomController>();
        StartCoroutine(SetCursorAfterMoment());
    }


    private IEnumerator SetCursorAfterMoment()
    {
        yield return new WaitForSeconds(1);
        SetCursor(_room.Cursor.CurrentAction);
    }

    void OnEnable()
    {
        _room.Cursor.OnChange += SetCursor;
        _room.Cursor.OnSelectedItemChanged += HandleSelectedItem;
    }

    void OnDisable()
    {
        _room.Cursor.OnChange -= SetCursor;
        _room.Cursor.OnSelectedItemChanged -= HandleSelectedItem;
    }

    public void Update()
    {
        if (Input.GetMouseButtonUp(0) && _room.Interactable == null)
        {
            _room.InteractionText.Clear();
        }
        if (Input.GetMouseButtonDown(1))
        {
            NextIcon();
        }
    }

    public void NextIcon() => _room.Cursor.Next();

    private void HandleSelectedItem(InventoryItemData item)
    {
        if (item == null) 
        { 
            _selectedImage.gameObject.SetActive(false); 
            return;
        }
        _selectedImage.sprite = item.Icon;
        _selectedImage.gameObject.SetActive(true);
    }

    private void SetCursor(CursorActionData cursor)
    {
        Cursor.SetCursor(cursor.CursorImage, Vector2.zero, CursorMode.Auto);   
    }
    
}