using UnityEngine;

[CreateAssetMenu]
public class CursorData : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public Texture2D CursorImage { get; private set;}

}