using UnityEngine;

namespace Projects.InventorySystem__Legacy_.Source
{
    public class ItemDatabase
    {
        private readonly ItemData[] _data = Resources.LoadAll<ItemData>("");

        public ItemData GetRandomItem()
        {
            return _data[Random.Range(0, _data.Length)];
        }
    }
}