using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Projects.Advanced_2D_Mechanics
{
    [RequireComponent(typeof(Collider2D))]
    [RequireComponent(typeof(Rigidbody2D))]
    public class Bullet : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;

        public async UniTask Init(Vector2 velocity, float lifetime)
        {
            _rigidbody2D.linearVelocity = velocity;
            await UniTask.WaitForSeconds(lifetime);
            Destroy(gameObject);
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            Destroy(gameObject);
        }
    }
}