using System;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;

namespace Projects.InventorySystem.Source
{
    public class InventoryView : MonoBehaviour
    {
        [Header("Prefabs")]
        [SerializeField] private Interactable _slotPrefab;
        [SerializeField] private Interactable _trashSlotPrefab;
        [SerializeField] private ItemView _itemPrefab;
        [Header("Components")]
        [SerializeField] private Transform _slotsContainer;
        [SerializeField] private Transform _itemsContainer;
        [SerializeField] private Interactable _dropZone;
        [Header("Drop animation")]
        [SerializeField] private Ease _dropEase = Ease.OutCubic;
        [SerializeField] private float _dropDuration = 0.2f;

        private readonly List<Interactable> _slotViews = new();
        private readonly List<ItemView> _itemViews = new();
        private ObjectPool<ItemView> _itemPool;

        private ItemView _draggedItem;
        private bool _isDragging;

        // public event Action<int> onSwitchAll;
        // public event Action<int> onSwitchOne;
        public event Action onDropAll;
        public event Action onDropOne;

        private void OnEnable()
        {
            _dropZone.onLeftClick += OnDropAll;
            _dropZone.onRightClick += OnDropOne;
        }

        private void OnDisable()
        {
            _dropZone.onLeftClick -= OnDropAll;
            _dropZone.onRightClick -= OnDropOne;
        }
        
        private void Update()
        {
            if (_isDragging) _draggedItem.transform.position = (Vector2)Input.mousePosition;
        }

        //todo
        // inject actions in controller
        public void RedrawSlots(DrawPackage[] slots)
        {
            _itemPool ??= new ObjectPool<ItemView>(() => Instantiate(_itemPrefab, _itemsContainer),
                item => item.gameObject.SetActive(true),
                item => item.gameObject.SetActive(false));
            _slotViews.Clear();
            _itemViews.Clear();

            for (var i = 0; i < slots.Length; i++)
            {
                var prefab = i < _slotViews.Count - 1 ? _slotPrefab : _trashSlotPrefab;
                var view = Instantiate(prefab, _slotsContainer);
                // view.onLeftClick += OnSwitchAll;
                // view.onRightClick += OnSwitchOne;
                DrawItem(slots[i]);
            }
        }

        private void DrawItem(DrawPackage package)
        {
            if (package is null) return;
            var view = _itemPool.Get();
            view.IconImage.sprite = package.Icon;
            view.AmountText.text = package.Amount;
            _itemViews.Add(view);
        }

        private void ReleaseItem(ItemView view)
        {
            _itemPool.Release(view);
            _itemViews.Remove(view);
        }

        private void OnDropAll() => onDropAll?.Invoke();
        private void OnDropOne() => onDropOne?.Invoke();
    }
}