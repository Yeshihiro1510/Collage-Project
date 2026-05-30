using System;
using UnityEngine;
using UnityEngine.UI;

namespace Projects.InventorySystem.Source
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private InventoryView _view;
        [SerializeField] private Button _spawnButton;
        [SerializeField] private Button _cleanButton;
        [SerializeField] private Button _deleteSaveButton;
        private InventoryModel _model;
        private ItemDatabase _itemDatabase;

        private void Awake()
        {
            _model = new InventoryModel();
            _itemDatabase = new ItemDatabase();

            _model.onSlotsChanged += _view.RedrawSlots;
            _model.onItemsChanged += _view.RedrawItems;
            _model.onChanged += ProgressSaver.Set;
            _view.onSwitch += _model.Switch;
            _view.onExtract += _model.Extract;
            _view.onDropAll += _model.DropAll;
            _view.onDropOne += _model.DropOne;
            _spawnButton.onClick.AddListener(() => _model.AddItemToAnyBaseSlot(_itemDatabase.GetRandomItem(), 1));
            _cleanButton.onClick.AddListener(_model.Clear);
            _deleteSaveButton.onClick.AddListener(ProgressSaver.Delete);

            _view.Initialize();
            if (ProgressSaver.TryGet(out var state) && state.slots.Length > 0) _model.Initialize(state);
            else _model.Initialize(25);
        }
    }
}