namespace Projects.InventorySystem.Source
{
    public class ItemBunch
    {
        public ItemBunch(ItemData data, int amount)
        {
            this.data = data;
            this.amount = amount;
        }

        public readonly ItemData data;
        public int amount;
        
        public bool IsFull => amount >= data.MaxStack;
    }
}