using UnityEngine;

public class CursorController : MonoBehaviour 
{
    [SerializeField]
    private CursorDatabase _cursors;

    void Start()
    {
        SetCursor(_cursors.Current);
    }

    public void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            NextIcon();
        }
    }

    public void NextIcon() => SetCursor(_cursors.Next());

    private void SetCursor(CursorData cursor)
    {
        Cursor.SetCursor(cursor.CursorImage, Vector2.zero, CursorMode.Auto);   
    }
    
}