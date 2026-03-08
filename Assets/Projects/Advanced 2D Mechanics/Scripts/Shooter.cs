using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Projects.Advanced_2D_Mechanics
{
    public class Shooter : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab;
        
        [SerializeField] private float _speed;
        [SerializeField] private float _amplitude;

        public void Shoot(Vector2 direction)
        {
            var obj = SinglePool.Instance.Get(_bulletPrefab);
            obj.transform.position = transform.position;
            obj.Launch(direction, _speed, _amplitude).Forget();
        }
    }
}