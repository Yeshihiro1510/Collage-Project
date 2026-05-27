using UnityEngine;
using UnityEngine.UI;

namespace Projects.InventorySystem.Source
{
    public class InventoryController : MonoBehaviour
    {
        [SerializeField] private InventoryView _view;
        [SerializeField] private Button _spawnButton;
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _cleanButton;

        private InventoryModel _model;
        private ItemDatabase _itemDatabase;

        private void Awake()
        {
            _model = new InventoryModel();
            _itemDatabase = new ItemDatabase();
            
            _model.onSlotsChanged += _view.Redraw;
            _view.onSwitch += _model.MouseSwitch;
            _view.onSeparate += _model.Separate;
            _spawnButton.onClick.AddListener(() => _model.AddItemToAnySlot(_itemDatabase.GetRandomItem()));
            _upgradeButton.onClick.AddListener(() => _model.SetSlots(1));

            _view.Initialize();
            _model.Initialize(23);
        }
    }
}