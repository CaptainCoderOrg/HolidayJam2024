using UnityEngine;

public class CursorController : MonoBehaviour 
{
    private RoomController _room;

    void Awake()
    {
        _room = GetComponentInParent<RoomController>();
    }

    void OnEnable()
    {
        SetCursor(_room.Cursor.Current);
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            NextIcon();
        }
    }

    public void NextIcon() => SetCursor(_room.Cursor.Next());

    private void SetCursor(CursorData cursor)
    {
        Cursor.SetCursor(cursor.CursorImage, Vector2.zero, CursorMode.Auto);   
    }
    
}