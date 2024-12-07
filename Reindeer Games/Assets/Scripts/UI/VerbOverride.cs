using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class VerbOverride : ScriptableObject
{
    public CursorActionData Cursor;
    public List<InventoryItemData> ItemMatch;
    public string Verb;
    public string Proposition;
}