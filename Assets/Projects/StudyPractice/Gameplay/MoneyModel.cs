using System;
using UnityEngine;

namespace Projects.StudyPractice.Gameplay
{
    [Serializable]
    public class MoneyModel
    {
        public MoneyModel(int money)
        {
            _money = money;
        }

        private int _money;

        public event Action<int> moneyChanged;

        public bool TryGet(int amount)
        {
            if (_money < amount) return false;
            _money -= amount;
            Update();
            return true;
        }

        public void Add(int amount)
        {
            _money += amount;
            Update();
        }

        public void Update()
        {
            moneyChanged?.Invoke(_money);
        }
    }
}