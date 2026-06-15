using UnityEngine;
using UnityEngine.UI;

namespace Projects.InventorySystem__Legacy_.Source
{
    public class InventoryControllerLegacy : MonoBehaviour
    {
        [SerializeField] private InventoryViewLegacy viewLegacy;
        [SerializeField] private Button _spawnButton;
        [SerializeField] private Button _cleanButton;
        [SerializeField] private Button _deleteSaveButton;
        private InventoryModel _model;
        private ItemDatabase _itemDatabase;

        private void Awake()
        {
            _model = new InventoryModel();
            _itemDatabase = new ItemDatabase();

            _model.onSlotsChanged += viewLegacy.RedrawSlots;
            _model.onItemsChanged += viewLegacy.RedrawItems;
            _model.onChanged += ProgressSaver.Set;
            viewLegacy.onSwitch += _model.Switch;
            viewLegacy.onExtract += _model.Extract;
            viewLegacy.onDropAll += _model.DropAll;
            viewLegacy.onDropOne += _model.DropOne;
            _spawnButton.onClick.AddListener(() => _model.AddItemToAnyBaseSlot(_itemDatabase.GetRandomItem(), 1));
            _cleanButton.onClick.AddListener(_model.Clear);
            _deleteSaveButton.onClick.AddListener(ProgressSaver.Delete);

            viewLegacy.Initialize();
            if (ProgressSaver.TryGet(out var state) && state.slots.Length > 0) _model.Initialize(state);
            else _model.Initialize(25);
        }
    }
}