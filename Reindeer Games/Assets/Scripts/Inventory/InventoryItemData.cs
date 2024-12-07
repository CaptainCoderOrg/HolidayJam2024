using UnityEngine;

[CreateAssetMenu(menuName = "Inventory/Item")]
public class InventoryItemData : ScriptableObject
{
    [field: SerializeField]
    public string Name { get; private set; }
    [field: SerializeField]
    public string Verb { get; private set; } = "Use";
    [field: SerializeField]
    public string Preposition { get; private set; } = "with";
    [field: SerializeField]
    public Sprite Icon { get; private set; }
    

}