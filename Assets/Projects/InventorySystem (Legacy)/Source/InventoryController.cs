using UnityEngine;

namespace Projects.InventorySystem__Legacy_.Source
{
    public class InventoryController
    {
        public InventoryController(InventoryWindowView windowView)
        {
            _model = new InventoryModel();

            _model.onSlotsChanged += windowView.RedrawSlots;
            _model.onItemsChanged += windowView.RedrawItems;
            _model.onChanged += ProgressSaver.Set;
            windowView.onSwitch += _model.Switch;
            windowView.onExtract += _model.Extract;
            windowView.onDropAll += _model.DropAll;
            windowView.onDropOne += _model.DropOne;

            windowView.Initialize();
            if (ProgressSaver.TryGet(out var state) && state.slots.Length > 0) _model.Initialize(state);
            else _model.Initialize(25);

            var food = Resources.LoadAll<ItemData>("");
            _model.AddItemToAnyBaseSlot(food[Random.Range(0, food.Length)], 1);
        }

        private readonly InventoryModel _model;
    }
}