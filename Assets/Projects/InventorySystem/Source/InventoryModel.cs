using System;

namespace Projects.InventorySystem.Source
{
    public class InventoryModel
    {
        private InventoryState _state;
        private int MouseBufferI => _state.slots.Length - 1;
        private int TrashSlotI => _state.slots.Length - 2;

        public event Action<InventoryState> onChanged;
        public event Action<ItemStack[]> onSlotsChanged;
        public event Action<(int i, ItemStack stack)[]> onItemsChanged;
        
        public void Initialize(InventoryState state)
        {
            _state = new InventoryState(new ItemStack[state.slots.Length]);
            for (var i = 0; i < state.slots.Length; i++)
            {
                var slot = state.slots[i];
                if (slot == ItemStack.Empty) _state.slots[i] = null;
                else _state.slots[i] = slot;
            }

            onSlotsChanged?.Invoke(_state.slots);
        }

        public void Initialize(int slots)
        {
            _state = new InventoryState(slots);
            SetSlots(slots);
        }

        public void DropAll()
        {
            var mouse = _state.slots[MouseBufferI];
            if (mouse == null) return;
            if (AddItemToAnyBaseSlot(mouse.data, mouse.amount))
            {
                _state.slots[MouseBufferI] = null;
                InvokeItemChanged(MouseBufferI);
            }
        }

        public void DropOne()
        {
            var mouse = _state.slots[MouseBufferI];
            if (mouse == null) return;
            if (mouse.Diff(1, out var result) && AddItemToAnyBaseSlot(mouse.data, 1))
            {
                _state.slots[MouseBufferI] = result;
                InvokeItemChanged(MouseBufferI);
            }
        }

        public bool AddItemToAnyBaseSlot(ItemData data, int amount)
        {
            if (amount > data.MaxStack) return false;

            int? emptySlot = null;
            for (var i = 0; i < TrashSlotI; i++)
            {
                var stack = _state.slots[i];
                if (stack is null) emptySlot ??= i;
                else if (stack.Sum(new ItemStack(data, amount), out var result))
                {
                    _state.slots[i] = result;
                    InvokeItemChanged(i);
                    return true;
                }
            }

            if (!emptySlot.HasValue) return false;
            _state.slots[emptySlot.Value] = new ItemStack(data, amount);
            InvokeItemChanged(emptySlot.Value);
            return true;
        }

        public void Extract(int from, int to)
        {
            var fromStack = _state.slots[from];
            var toStack = _state.slots[to];
            if (fromStack is null) return;
            if (fromStack.Diff(1, out var fromResult))
            {
                if (toStack is null)
                {
                    _state.slots[to] = new ItemStack(fromStack.data, 1);
                    _state.slots[from] = fromResult;
                }
                else if (toStack.Sum(new ItemStack(fromStack.data, 1), out var result))
                {
                    _state.slots[to] = result;
                    _state.slots[from] = fromResult;
                }
            }

            InvokeItemChanged(from, to);
        }

        public void Switch(int first, int second)
        {
            var firstStack = _state.slots[first];
            var secondStack = _state.slots[second];

            if (firstStack != null && secondStack != null && secondStack.Sum(firstStack, out var result))
            {
                _state.slots[first] = null;
                _state.slots[second] = result;
            }
            else
            {
                if (second == TrashSlotI && firstStack is not null)
                    (_state.slots[first], _state.slots[second]) = (null, _state.slots[first]);
                else if (first == TrashSlotI && secondStack is not null)
                    (_state.slots[first], _state.slots[second]) = (_state.slots[second], null);
                else (_state.slots[first], _state.slots[second]) = (_state.slots[second], _state.slots[first]);
            }

            InvokeItemChanged(first, second);
        }

        public void Clear()
        {
            for (var i = 0; i < _state.slots.Length; i++)
            {
                _state.slots[i] = null;
                InvokeItemChanged(i);
            }
        }

        private void SetSlots(int amount)
        {
            if (amount < 2) amount = 2;
            var newSlots = new ItemStack[amount];
            if (_state.slots is not null) Array.Copy(_state.slots, 0, newSlots, 0, amount);
            _state = new InventoryState(newSlots);
            onSlotsChanged?.Invoke(_state.slots);
        }

        private void InvokeItemChanged(params int[] indexes)
        {
            if (indexes.Length < 1) return;
            var data = new (int, ItemStack)[indexes.Length];
            for (var i = 0; i < indexes.Length; i++) data[i] = (indexes[i], _state.slots[indexes[i]]);
            onItemsChanged?.Invoke(data);
            onChanged?.Invoke(new InventoryState(_state.slots.Clone() as ItemStack[]));
        }
    }
}