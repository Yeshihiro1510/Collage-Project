using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Pool;

namespace Projects.InventorySystem__Legacy_.Source
{
    public class InventoryViewLegacy : MonoBehaviour
    {
        [Header("Components")]
        [SerializeField] private Transform _slotsParent;
        [SerializeField] private Transform _itemsParent;
        [SerializeField] private Transform _itemsPoolParent;
        [SerializeField] private Transform _draggingParent;
        [SerializeField] private Interactable _dropZone;
        [SerializeField] private Interactable _slotViewPrefab;
        [SerializeField] private Interactable _trashPrefab;
        [SerializeField] private ItemView _itemViewPrefab;

        [Header("Drop animation")]
        [SerializeField] private Ease _dropEase = Ease.OutCubic;
        [SerializeField] private float _dropDuration = 0.2f;

        private Interactable[] _slotViews = Array.Empty<Interactable>();
        private ItemView[] _itemViews = Array.Empty<ItemView>();
        private ObjectPool<ItemView> _itemPool;

        public int MouseBufferI => _itemViews.Length - 1;
        public int TrashSlotI => _slotViews.Length - 1;

        public event Action<int, int> onSwitch;
        public event Action<int, int> onExtract;
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

        public void Initialize()
        {
            _itemPool = new ObjectPool<ItemView>(() => Instantiate(_itemViewPrefab),
                item => item.gameObject.SetActive(true),
                item =>
                {
                    item.gameObject.SetActive(false);
                    item.transform.SetParent(_itemsPoolParent, true);
                });
        }

        private void Update()
        {
            if (_itemViews is { Length: > 0 } && _itemViews[^1] is not null) _itemViews[^1].transform.position = Input.mousePosition;
            for (var i = 0; i < _slotViews.Length; i++)
            {
                if (_itemViews[i] is not null) _itemViews[i].transform.position = _slotViews[i].transform.position;
            }
        }

        public void RedrawItems((int i, ItemStack stack)[] data)
        {
            foreach (var pair in data)
            {
                if (pair.stack is null)
                {
                    if (_itemViews[pair.i]) DestroyItem(pair.i);
                }
                else if (pair.stack != ItemStack.Empty)
                {
                    if (_itemViews[pair.i])
                    {
                        _itemViews[pair.i].IconImage.sprite = pair.stack.data.Icon;
                        _itemViews[pair.i].AmountText.text = pair.stack.amount.ToString();
                    }
                    else CreateItem(pair.stack.data.Icon, pair.stack.amount, pair.i);
                }
            }
        }

        public void RedrawSlots(ItemStack[] states)
        {
            for (var i = 0; i < _itemViews.Length; i++)
            {
                if (i < _slotViews.Length) Destroy(_slotViews[i].gameObject);
                if (_itemViews[i]) DestroyItem(i);
            }

            _slotViews = new Interactable[states.Length - 1];
            _itemViews = new ItemView[states.Length];

            for (var i = 0; i < _slotViews.Length; i++)
            {
                if (i < MouseBufferI) CreateSlot(i);
                if (states[i] is not null) CreateItem(states[i].data.Icon, states[i].amount, i);
            }
        }

        private ItemView CreateItem(Sprite icon, int count, int i)
        {
            var view = _itemPool.Get();
            view.IconImage.sprite = icon;
            view.AmountText.text = count.ToString();
            _itemViews[i] = view;
            if (i < MouseBufferI)
            {
                view.transform.SetParent(_itemsParent);
                view.transform.position = _slotViews[i].transform.position;
            }
            else
            {
                view.transform.SetParent(_draggingParent);
            }

            return view;
        }

        private void DestroyItem(int i)
        {
            _itemPool.Release(_itemViews[i]);
            _itemViews[i] = null;
        }

        private Interactable CreateSlot(int i)
        {
            var prefab = i < TrashSlotI ? _slotViewPrefab : _trashPrefab;
            var view = Instantiate(prefab, _slotsParent);
            view.onLeftClick += OnLeftClick;
            view.onRightClick += OnRightClick;
            _slotViews[i] = view;
            return view;
        }

        private void OnDropAll(Interactable _)
        {
            onDropAll?.Invoke();
        }

        private void OnDropOne(Interactable _)
        {
            onDropOne?.Invoke();
        }

        private void OnLeftClick(Interactable slot)
        {
            onSwitch?.Invoke(MouseBufferI, Array.IndexOf(_slotViews, slot));
            print("Left click");
        }

        private void OnRightClick(Interactable slot)
        {
            onExtract?.Invoke(MouseBufferI, Array.IndexOf(_slotViews, slot));
            print("Right click");
        }
    }
}