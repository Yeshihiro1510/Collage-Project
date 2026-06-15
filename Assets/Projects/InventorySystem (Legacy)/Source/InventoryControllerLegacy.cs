using UnityEngine;

namespace Projects.InventorySystem__Legacy_.Source
{
    public class InventoryControllerLegacy
    {
        public InventoryControllerLegacy(InventoryViewLegacy view)
        {
            _model = new InventoryModel();

            _model.onSlotsChanged += view.RedrawSlots;
            _model.onItemsChanged += view.RedrawItems;
            _model.onChanged += ProgressSaver.Set;
            view.onSwitch += _model.Switch;
            view.onExtract += _model.Extract;
            view.onDropAll += _model.DropAll;
            view.onDropOne += _model.DropOne;
            // _spawnButton.onClick.AddListener(() => _model.AddItemToAnyBaseSlot(_itemDatabase.GetRandomItem(), 1));
            // _cleanButton.onClick.AddListener(_model.Clear);
            // _deleteSaveButton.onClick.AddListener(ProgressSaver.Delete);

            if (ProgressSaver.TryGet(out var state) && state.slots.Length > 0) _model.Initialize(state);
            else _model.Initialize(25);

            var food = Resources.LoadAll<ItemData>("");
            _model.AddItemToAnyBaseSlot(food[Random.Range(0, food.Length)], 1);
        }

        private readonly InventoryModel _model;
    }
}