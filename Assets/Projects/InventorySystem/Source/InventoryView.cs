using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;

namespace Projects.InventorySystem.Source
{
    public class InventoryView : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform _slotsContainer;
        [SerializeField] private Transform _itemsContainer;
        [SerializeField] private Transform _itemsPool;
        [SerializeField] private Interactable _dropZone;
        [SerializeField] private Interactable _trashSlot;
        private Interactable _slotViewPrefab;
        private ItemView _itemViewPrefab;

        [Header("Drop animation")]
        [SerializeField] private Ease _dropEase = Ease.OutCubic;
        [SerializeField] private float _dropDuration = 0.2f;

        private (Interactable, ItemView)[] _slots = Array.Empty<(Interactable, ItemView)>();
        private int _draggingIndex = -1;
        private ObjectPool<ItemView> _itemPool;

        public event Action<int, int> onSwitch;
        public event Action<int, int> onSeparate;

        private void OnEnable()
        {
            _dropZone.onLeftClick += OnDrop;
            _dropZone.onRightClick += OnDrop;
        }

        private void OnDisable()
        {
            _dropZone.onLeftClick -= OnDrop;
            _dropZone.onRightClick -= OnDrop;
        }

        public void Initialize()
        {
            _slotViewPrefab = Resources.Load<Interactable>("SlotView");
            _itemViewPrefab = Resources.Load<ItemView>("ItemView");
            _itemPool = new ObjectPool<ItemView>(() => Instantiate(_itemViewPrefab),
                item =>
                {
                    item.gameObject.SetActive(true);
                    item.transform.SetParent(_itemsContainer, true);
                },
                item =>
                {
                    item.gameObject.SetActive(false);
                    item.transform.SetParent(_itemsPool, true);
                });
        }

        private void Update()
        {
            if (_draggingIndex >= 0 && _slots[_draggingIndex].Item2) _slots[_draggingIndex].Item2.transform.position = Input.mousePosition;
        }

        public void Redraw(ItemBunch?[] slots)
        {
            if (_slots.Length != slots.Length)
            {
                RedrawAtAll(slots);
                return;
            }

            for (var i = 0; i < slots.Length; i++)
            {
                if (slots[i] is null)
                {
                    if (_slots[i].Item2 is not null)
                    {
                        _itemPool.Release(_slots[i].Item2);
                        _slots[i].Item2 = null;
                    }
                    continue;
                }

                if (_slots[i].Item2 is null)
                {
                    CreateItem(slots[i].Value.Data.Icon, slots[i].Value.Amount, i);
                }
                else
                {
                    _slots[i].Item2.Icon.sprite = slots[i].Value.Data.Icon;
                    _slots[i].Item2.Amount.text = slots[i].Value.Amount.ToString();
                }
            }
        }

        private void RedrawAtAll(ItemBunch?[] slots)
        {
            for (var i = 0; i < _slots.Length; i++)
            {
                Destroy(_slots[i].Item1.gameObject);
                if (_slots[i].Item2) _itemPool.Release(_slots[i].Item2);
            }

            _slots = new (Interactable, ItemView)[slots.Length];

            for (var i = 0; i < slots.Length; i++)
            {
                var slotView = Instantiate(_slotViewPrefab, _slotsContainer);
                _slots[i].Item1 = slotView;
                slotView.onLeftClick += OnLeftClick;
                slotView.onRightClick += OnRightClick;
                slotView.onLeftClick += _ => print("left click");

                if (slots[i] is not null)
                {
                    CreateItem(slots[i].Value.Data.Icon, slots[i].Value.Amount, i);
                }
            }
        }

        private ItemView CreateItem(Sprite icon, int count, int slot)
        {
            var view = _itemPool.Get();
            view.Icon.sprite = icon;
            view.Amount.text = count.ToString();
            view.transform.position = _slots[slot].Item1.transform.position;
            _slots[slot].Item2 = view;
            return view;
        }

        private void OnDrop(Interactable _)
        {
            if (_draggingIndex >= 0)
            {
                var pair = _slots[_draggingIndex];
                pair.Item2.transform.DOMove(pair.Item1.transform.position, _dropDuration).SetEase(_dropEase);
                _draggingIndex = -1;
            }
        }

        private void OnLeftClick(Interactable slot)
        {
            var i = IndexOf(slot);
            if (_draggingIndex < 0)
            {
                _draggingIndex = i;
            }
            else if (_draggingIndex == i)
            {
                _draggingIndex = -1;
                _slots[i].Item2.transform.position = slot.transform.position;
            }
            else
            {
                onSwitch?.Invoke(_draggingIndex, i);
            }
        }

        private void OnRightClick(Interactable slot)
        {
            if (_draggingIndex >= 0)
            {
                onSeparate?.Invoke(_draggingIndex, IndexOf(slot));
            }
        }

        private int IndexOf(Interactable slot)
        {
            for (var i = 0; i < _slots.Length; i++)
            {
                if (_slots[i].Item1 == slot)
                {
                    return i;
                }
            }

            return -1;
        }
    }
}