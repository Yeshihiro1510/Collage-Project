using System;

namespace Projects.InventorySystem.Source.New
{
    [Serializable]
    public readonly struct ItemStack : IEquatable<ItemStack>
    {
        public static readonly ItemStack Empty = new(null, 0);

        public ItemStack(ItemData data, int amount)
        {
            Data = data;
            Amount = amount;
        }

        public readonly ItemData Data;
        public readonly int Amount;
        
        public static ItemStack operator +(ItemStack a, ItemStack b)
        {
            if (a.Data != b.Data) throw new Exception($"ItemStack: {b} has different item data: {a.Data} != {b.Data}.");
            var sum = a.Amount + b.Amount;
            if (sum > a.Data.MaxStack) throw new Exception($"The sum must be less than max stack: {a.Data.MaxStack}.");
            return new ItemStack(a.Data, sum);
        }

        public static ItemStack operator -(ItemStack a, ItemStack b)
        {
            if (a.Data != b.Data) throw new Exception($"ItemStack: {b} has different item data: {a.Data} != {b.Data}.");
            var diff = a.Amount - b.Amount;
            if (diff < 0) throw new Exception($"The diff must be non negative: {diff}.");
            return diff < 1 ? Empty : new ItemStack(a.Data, diff);
        }
        
        public bool Equals(ItemStack other)
        {
            return Equals(Data, other.Data) && Amount == other.Amount;
        }

        public override bool Equals(object obj)
        {
            return obj is ItemStack other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Data, Amount);
        }
    }
}