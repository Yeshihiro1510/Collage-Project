using System;
using UnityEngine;

namespace Projects.Advanced_2D_Mechanics
{
    public class Killable : MonoBehaviour
    {
        [SerializeField] private float _healthPoints;

        public bool IsDead { get; private set; }

        public event Action onDeath;

        public void Damage()
        {
            if (IsDead) return;

            _healthPoints -= 1;

            if (_healthPoints == 0)
            {
                IsDead = true;
                onDeath?.Invoke();
                Destroy(gameObject);
            }
        }
    }
}