using System;

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
            if (_money <= amount) return false;
            _money -= amount;
            moneyChanged?.Invoke(_money);
            return true;
        }

        public void Add(int amount)
        {
            _money += amount;
            moneyChanged?.Invoke(_money);
        }
    }
}