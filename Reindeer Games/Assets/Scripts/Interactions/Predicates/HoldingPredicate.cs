using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Predicate/Holding")]
public class HoldingPredicate : Predicate
{
    public List<InventoryItemData> ItemMatch;
    public override bool IsMet(CursorData cursorData) => ItemMatch.Contains(cursorData.SelectedItem);
}