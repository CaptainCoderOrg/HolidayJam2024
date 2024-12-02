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
        _room.Cursor.OnChange += SetCursor;
    }

    void OnDisable()
    {
        _room.Cursor.OnChange -= SetCursor;
    }

    private readonly YieldInstruction Wait = new WaitForFixedUpdate();

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

    private void SetCursor(CursorData cursor)
    {
        Cursor.SetCursor(cursor.CursorImage, Vector2.zero, CursorMode.Auto);   
    }
    
}