using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CursorController : MonoBehaviour 
{
    [field: SerializeField]
    public Texture2D GenericCursor { get; private set; }
    private RoomController _room;

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
    }

    void OnDisable()
    {
        _room.Cursor.OnChange -= SetCursor;
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


    private void SetCursor(CursorActionData cursor)
    {
        Cursor.SetCursor(cursor.CursorImage, Vector2.zero, CursorMode.Auto);   
    }
    
}