using UnityEngine;

public class CursorFollower : MonoBehaviour
{

    [field: SerializeField]
    public Vector3 Offset { get; private set;}
    void Update()
    {
        transform.position = Input.mousePosition + Offset;
    }
}