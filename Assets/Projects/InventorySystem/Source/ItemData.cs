using UnityEngine;

namespace Projects.InventorySystem.Source
{
    [CreateAssetMenu(menuName = "Item Data")]
    public class ItemData : ScriptableObject
    {
        [field: SerializeField] public Sprite Icon { get; private set; }
        [field: SerializeField] public string Name { get; private set; }
        [field: SerializeField] public int MaxStack { get; private set; }
        [field: SerializeField] public string Commentary { get; private set; }
    }
}