using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class CursorActionData : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public string DefaultVerb { get; private set; }
    [field: SerializeField]
    public string DefaultProposition { get; private set; } = string.Empty;
    [field: SerializeField]
    public Texture2D CursorImage { get; private set;}
    [field: SerializeField]
    public List<string> DefaultMessages { get; private set; }

}