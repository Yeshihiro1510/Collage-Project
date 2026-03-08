using UnityEngine;
using Yeshi_Pool;

namespace Projects.Advanced_2D_Mechanics
{
    public class DestroyVFX : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _VFX;
        [SerializeField] private MonoPoolable _lootPrefab;
        [SerializeField] private Killable _killable;

        [SerializeField] private float _lootAmount;

        private void OnEnable()
        {
            _killable.onDeath += OnDeath;
        }

        private void OnDisable()
        {
            _killable.onDeath -= OnDeath;
        }

        private void OnDeath()
        {
            for (int i = 0; i < _lootAmount; i++)
            {
                var obj = SinglePool.Instance.Get(_lootPrefab);
                obj.transform.position = transform.position;
                obj.GetComponent<Rigidbody2D>().linearVelocity = Random.insideUnitCircle;
            }
        }
    }
}