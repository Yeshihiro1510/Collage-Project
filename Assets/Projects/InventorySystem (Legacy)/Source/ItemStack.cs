using System;

namespace Projects.InventorySystem__Legacy_.Source
{
    [Serializable]
    public record ItemStack
    {
        public static readonly ItemStack Empty = new(null, 0);

        public ItemStack(ItemData data, int amount)
        {
            this.data = data;
            this.amount = amount;
        }

        public ItemData data;
        public int amount;
    }
}