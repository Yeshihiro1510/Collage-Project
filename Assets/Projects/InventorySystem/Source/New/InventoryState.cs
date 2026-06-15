using System;

namespace Projects.InventorySystem.Source.New
{
    [Serializable]
    public class InventoryState
    {
        public const int MIN_AMOUNT = 3;
        
        public InventoryState(int slots)
        {
            Slots = slots < MIN_AMOUNT ? new ItemStack[MIN_AMOUNT] : new ItemStack[slots];
        }

        public int MouseBuffer => Slots.Length - 1;
        public int TrashBuffer => Slots.Length - 2;
        public ItemStack[] Slots { get; private set; }

        public bool IsEmpty(int i) => Slots[i].Equals(ItemStack.Empty);
    }
}