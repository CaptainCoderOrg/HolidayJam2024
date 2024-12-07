using UnityEngine;

public class CursorFollower : MonoBehaviour 
{
    void Update()
    {
        transform.position = Input.mousePosition;
    }
}