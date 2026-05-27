using System;

namespace Projects.InventorySystem.Source
{
    public class InventoryModel
    {
        private ItemBunch[] _slots;
        private ItemBunch _MouseBuffer => _slots[^1];
        private ItemBunch _TrashSlot => _slots[^2];

        public event Action<ItemTransferData[]> onSlotsChanged;
        public event Action<ItemTransferData> onItemChanged;
        public event Action<ItemTransferData> onMouseBufferChanged;
        public event Action<ItemTransferData> onTrashChanged;

        public void Initialize(int slots)
        {
            SetSlots(slots);
        }

        public bool AddItemToAnySlot(ItemData item)
        {
            int? emptySlot = null;
            for (var i = 0; i < _slots.Length; i++)
            {
                if (_slots[i] == null)
                {
                    emptySlot ??= i;
                }
                else if (_slots[i].data == item && !_slots[i].IsFull)
                {
                    _slots[i] = new ItemBunch(item, _slots[i].amount + 1);
                    onSlotsChanged?.Invoke(_slots);
                    return true;
                }
            }

            if (emptySlot.HasValue)
            {
                _slots[emptySlot.Value] = new ItemBunch(item, 1);
                onSlotsChanged?.Invoke(_slots);
                return true;
            }

            return false;
        }


        public void Separate(int i)
        {
            if (_MouseBuffer is null) return;
            if (Minus(_MouseBuffer))
        }

        public void MouseSwitch(int i)
        {
            (_slots[i], _MouseBuffer) = (_MouseBuffer, _slots[i]);
            InvokeItemChanged(i);
            InvokeMouseBufferChanged();
        }
        
        public void ClearSlots()
        {
            if (_slots is null) return;
            for (var i = 0; i < _slots.Length; i++)
            {
                _slots[i] = null;
            }

            InvokeSlotsChanged();
        }

        private bool Add(int i, ItemData data)
        {
            if (_slots[i] is null)
            {
                SetItem(i, data, 1);
                return true;
            }

            if (_slots[i].data == data && !_slots[i].IsFull)
            {
                SetItem(i, data, _slots[i].amount + 1);
                return true;
            }

            return false;
        }

        private bool Minus(int i)
        {
            if (_slots[i] is null) return false;
            switch (_slots[i].amount)
            {
                case < 0:
                    DeleteItem(i);
                    return false;
                case < 2:
                    DeleteItem(i);
                    return true;
                case > 1:
                    SetItem(i, _slots[i].data, _slots[i].amount - 1);
                    return true;
            }
        }

        private void SetItem(int i, ItemData data, int amount)
        {
            _slots[i] = new ItemBunch(data, amount);
            InvokeItemChanged(i);
        }

        private void DeleteItem(int i)
        {
            _slots[i] = null;
            InvokeItemChanged(i);
        }

        private void SetSlots(int amount)
        {
            if (_slots is null)
            {
                _slots = new ItemBunch[amount];
            }
            else
            {
                var newSlots = new ItemBunch[_slots.Length + amount];
                Array.Copy(_slots, 0, newSlots, 0, newSlots.Length);
                _slots = newSlots;
            }

            InvokeSlotsChanged();
        }

        private void InvokeTrashChanged() =>
            onTrashChanged?.Invoke(new ItemTransferData(_TrashSlot.data, _TrashSlot.amount, -1));

        private void InvokeMouseBufferChanged() =>
            onMouseBufferChanged?.Invoke(new ItemTransferData(_MouseBuffer.data, _MouseBuffer.amount, -1));

        private void InvokeItemChanged(int i) =>
            onItemChanged?.Invoke(new ItemTransferData(_slots[i].data, _slots[i].amount, i));

        private void InvokeSlotsChanged()
        {
            var data = new ItemTransferData[_slots.Length];
            for (var i = 0; i < _slots.Length; i++)
            {
                var itemBunch = _slots[i];
                data[i] = new ItemTransferData(itemBunch.data, itemBunch.amount, i);
            }

            onSlotsChanged?.Invoke(data);
        }

        // private void InvokeItemsChanged(params int[] indexes)
        // {
        //     var data = new ItemTransferData[indexes.Length];
        //     foreach (var i in indexes)
        //     {
        //         var itemBunch = _slots[i];
        //         data[i] = new ItemTransferData(itemBunch.data, itemBunch.amount, i);
        //     }
        //
        //     onItemChanged?.Invoke(data);
        // }
    }

    public record ItemTransferData
    {
        public ItemTransferData(ItemData data, int amount, int slot)
        {
            Data = data;
            Amount = amount;
            Slot = slot;
        }

        public readonly ItemData Data;
        public readonly int Amount;
        public readonly int Slot;
    }
}