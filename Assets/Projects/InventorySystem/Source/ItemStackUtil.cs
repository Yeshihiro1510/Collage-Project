namespace Projects.InventorySystem.Source
{
    public static class ItemStackUtil
    {
        public static bool Sum(this ItemStack origin, ItemStack stack, out ItemStack result)
        {
            if (origin.data != stack.data)
            {
                result = origin;
                return false;
            }

            var sum = origin.amount + stack.amount;
            if (sum <= origin.data.MaxStack)
            {
                result = new ItemStack(origin.data, sum);
                return true;
            }

            result = origin;
            return false;
        }

        public static bool Sum(this ItemStack origin, int amount, out ItemStack result)
        {
            var sum = origin.amount + amount;
            if (sum <= origin.data.MaxStack)
            {
                result = new ItemStack(origin.data, sum);
                return true;
            }

            result = origin;
            return false;
        }

        public static bool Diff(this ItemStack origin, ItemStack stack, out ItemStack result)
        {
            if (origin.data != stack.data)
            {
                result = origin;
                return false;
            }

            var diff = origin.amount - stack.amount;
            switch (diff)
            {
                case < 0:
                    result = origin;
                    return false;
                case 0:
                    result = null;
                    return true;
                case > 0:
                    result = new ItemStack(origin.data, diff);
                    return true;
            }
        }

        public static bool Diff(this ItemStack origin, int amount, out ItemStack result)
        {
            var diff = origin.amount - amount;
            switch (diff)
            {
                case < 0:
                    result = origin;
                    return false;
                case 0:
                    result = null;
                    return true;
                case > 0:
                    result = new ItemStack(origin.data, diff);
                    return true;
            }
        }
    }
}