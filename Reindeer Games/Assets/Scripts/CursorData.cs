using UnityEngine;

[CreateAssetMenu]
public class CursorData : ScriptableObject
{
    [field: SerializeField]
    public Texture2D CursorImage { get; private set;}

}
