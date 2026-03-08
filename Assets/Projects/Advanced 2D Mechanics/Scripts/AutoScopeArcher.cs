using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Projects.Advanced_2D_Mechanics
{
    public class AutoScopeArcher : MonoBehaviour
    {
        [SerializeField] private Shooter _shooter;
        [SerializeField] private Transform _target;

        [SerializeField] private float _radius;
        [SerializeField] private float _delay;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, _radius);
        }

        private async UniTask Start()
        {
            while (!destroyCancellationToken.IsCancellationRequested)
            {
                if (_target != null && Vector2.Distance(transform.position, _target.position) < _radius)
                    _shooter.Shoot(_target.position - transform.position);

                await UniTask.WaitForSeconds(_delay, cancellationToken: destroyCancellationToken);
            }
        }
    }
}