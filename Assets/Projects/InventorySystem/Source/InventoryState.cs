using System;

namespace Projects.InventorySystem.Source
{
    [Serializable]
    public class InventoryState
    {
        public InventoryState(int slots)
        {
            this.slots = new ItemStack[slots];
        }

        public InventoryState(ItemStack[] slots)
        {
            this.slots = slots;
        }

        public ItemStack[] slots;
    }
}