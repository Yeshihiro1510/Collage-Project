using System;
using Projects.InventorySystem.Source.New;

namespace Projects.InventorySystem.Source
{
    public class InventoryModel
    {
        private InventoryState _state;
        private int MouseBufferI => _state.Slots.Length - 1;
        private int TrashSlotI => _state.Slots.Length - 2;

        public event Action<InventoryState> onChanged;
        public event Action<ItemStack[]> onSlotsChanged;
        public event Action<(int i, ItemStack stack)[]> onItemsChanged;
        
        public void Initialize(InventoryState state)
        {
            _state = new InventoryState(new ItemStack[state.Slots.Length]);
            for (var i = 0; i < state.Slots.Length; i++)
            {
                var slot = state.Slots[i];
                if (slot == ItemStack.Empty) _state.Slots[i] = null;
                else _state.Slots[i] = slot;
            }

            onSlotsChanged?.Invoke(_state.Slots);
        }

        public void Initialize(int slots)
        {
            _state = new InventoryState(slots);
            SetSlots(slots);
        }

        public void DropAll()
        {
            var mouse = _state.Slots[MouseBufferI];
            if (mouse == null) return;
            if (AddItemToAnyBaseSlot(mouse.data, mouse.amount))
            {
                _state.Slots[MouseBufferI] = null;
                InvokeItemChanged(MouseBufferI);
            }
        }

        public void DropOne()
        {
            var mouse = _state.Slots[MouseBufferI];
            if (mouse == null) return;
            if (mouse.Diff(1, out var result) && AddItemToAnyBaseSlot(mouse.data, 1))
            {
                _state.Slots[MouseBufferI] = result;
                InvokeItemChanged(MouseBufferI);
            }
        }

        public bool AddItemToAnyBaseSlot(ItemData data, int amount)
        {
            if (amount > data.MaxStack) return false;

            int? emptySlot = null;
            for (var i = 0; i < TrashSlotI; i++)
            {
                var stack = _state.Slots[i];
                if (stack is null) emptySlot ??= i;
                else if (stack.Sum(new ItemStack(data, amount), out var result))
                {
                    _state.Slots[i] = result;
                    InvokeItemChanged(i);
                    return true;
                }
            }

            if (!emptySlot.HasValue) return false;
            _state.Slots[emptySlot.Value] = new ItemStack(data, amount);
            InvokeItemChanged(emptySlot.Value);
            return true;
        }

        public void Extract(int from, int to)
        {
            var fromStack = _state.Slots[from];
            var toStack = _state.Slots[to];
            if (fromStack is null) return;
            if (fromStack.Diff(1, out var fromResult))
            {
                if (toStack is null)
                {
                    _state.Slots[to] = new ItemStack(fromStack.data, 1);
                    _state.Slots[from] = fromResult;
                }
                else if (toStack.Sum(new ItemStack(fromStack.data, 1), out var result))
                {
                    _state.Slots[to] = result;
                    _state.Slots[from] = fromResult;
                }
            }

            InvokeItemChanged(from, to);
        }

        public void Switch(int first, int second)
        {
            var firstStack = _state.Slots[first];
            var secondStack = _state.Slots[second];

            if (firstStack != null && secondStack != null && secondStack.Sum(firstStack, out var result))
            {
                _state.Slots[first] = null;
                _state.Slots[second] = result;
            }
            else
            {
                if (second == TrashSlotI && firstStack is not null)
                    (_state.Slots[first], _state.Slots[second]) = (null, _state.Slots[first]);
                else if (first == TrashSlotI && secondStack is not null)
                    (_state.Slots[first], _state.Slots[second]) = (_state.Slots[second], null);
                else (_state.Slots[first], _state.Slots[second]) = (_state.Slots[second], _state.Slots[first]);
            }

            InvokeItemChanged(first, second);
        }

        public void Clear()
        {
            for (var i = 0; i < _state.Slots.Length; i++)
            {
                _state.Slots[i] = null;
                InvokeItemChanged(i);
            }
        }

        private void SetSlots(int amount)
        {
            if (amount < 2) amount = 2;
            var newSlots = new ItemStack[amount];
            if (_state.Slots is not null) Array.Copy(_state.Slots, 0, newSlots, 0, amount);
            _state = new InventoryState(newSlots);
            onSlotsChanged?.Invoke(_state.Slots);
        }

        private void InvokeItemChanged(params int[] indexes)
        {
            if (indexes.Length < 1) return;
            var data = new (int, ItemStack)[indexes.Length];
            for (var i = 0; i < indexes.Length; i++) data[i] = (indexes[i], _state.Slots[indexes[i]]);
            onItemsChanged?.Invoke(data);
            onChanged?.Invoke(new InventoryState(_state.Slots.Clone() as ItemStack[]));
        }
    }
}