using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Projects.InventorySystem__Legacy_.Source;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Projects.StudyPractice.Gameplay
{
    public class ShopSystem
    {
        public const int ITEMS_AMOUNT = 3;
        public const float TIME_TO_UPDATE = 10f;

        private readonly ItemData[] _items = Resources.LoadAll<ItemData>("");

        public event Action<ItemData[]> updated;
        public event Action<float> tick;

        public IEnumerator Routine()
        {
            while (true)
            {
                Update();
                for (var time = 0f; time < TIME_TO_UPDATE; time += Time.deltaTime)
                {
                    tick?.Invoke(time);
                    yield return null;
                }
            }
        }

        private void Update()
        {
            var list = _items.ToList();
            var result = new List<ItemData>();
            for (var i = 0; i < ITEMS_AMOUNT; i++)
            {
                var index = Random.Range(0, list.Count);
                result.Add(list[index]);
                list.RemoveAt(index);
            }

            updated?.Invoke(result.ToArray());
        }
    }
}