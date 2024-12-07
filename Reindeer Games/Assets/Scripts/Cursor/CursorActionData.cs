using UnityEngine;

[CreateAssetMenu]
public class CursorActionData : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public string DefaultVerb { get; private set; }
    [field: SerializeField]
    public Texture2D CursorImage { get; private set;}

}