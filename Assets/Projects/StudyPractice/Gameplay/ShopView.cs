using System.Collections.Generic;
using Projects.InventorySystem__Legacy_.Source;
using Projects.StudyPractice.VFX;
using UnityEngine;
using UnityEngine.Pool;

namespace Projects.StudyPractice.Gameplay
{
    public class ShopView : RotationAnimationPopup
    {
        [SerializeField] private Transform _content;
        private ObjectPool<ShopCardView> _pool;
        private readonly List<ShopCardView> _views = new();

        public void Initialize()
        {
            base.Initialize(transform.position);
            var prefab = Resources.Load<ShopCardView>("CardView");
            _pool = new ObjectPool<ShopCardView>(
                () => Instantiate(prefab, _content),
                v => v.gameObject.SetActive(true),
                v => v.gameObject.SetActive(false));
        }

        public void Draw(params ItemData[] data)
        {
            foreach (var itemData in data)
            {
                var view = _pool.Get();
                view.Init(itemData.Name, itemData.Commentary, itemData.Icon);
                _views.Add(view);
            }
        }

        public void Clear()
        {
            foreach (var view in _views) _pool.Release(view);
            _views.Clear();
        }
    }
}