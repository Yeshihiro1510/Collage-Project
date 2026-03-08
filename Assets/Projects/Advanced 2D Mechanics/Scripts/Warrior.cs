using Cysharp.Threading.Tasks;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Projects.Advanced_2D_Mechanics
{
    public class Warrior : MonoBehaviour
    {
        [SerializeField] private TopDownMovement _movement;
        [SerializeField] private Animator _animator;
        [SerializeField] private Killable _target;

        [SerializeField] private float _guardRadius;
        [SerializeField] private float _attackRadius;
        [SerializeField] private Vector2 _radiusOffset;

        private bool _attackInProgress;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere((Vector2)transform.position + _radiusOffset, _guardRadius);
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere((Vector2)transform.position + _radiusOffset, _attackRadius);
        }

        private void Update()
        {
            if (!_target) return;
            var thisPosition = (Vector2)transform.position;
            var distance = Vector2.Distance(thisPosition + _radiusOffset, _target.transform.position);
            if (distance <= _guardRadius)
            {
                _movement.Move((Vector2)_target.transform.position - (thisPosition + _radiusOffset));
            }
            else
            {
                _movement.Stop();
                return;
            }

            if (distance <= _attackRadius && !_attackInProgress) Hit().Forget();
            if (distance <= _attackRadius * .8f) _movement.Stop();
        }

        private async UniTask Hit()
        {
            _attackInProgress = true;
            _animator.Play(Animator.StringToHash(Random.Range(0, 2) == 0 ? "Attack 1" : "Attack 2"));

            await UniTask.WaitForSeconds(_animator.GetCurrentAnimatorStateInfo(0).length * .9f,
                cancellationToken: this.GetCancellationTokenOnDestroy());

            if (Vector2.Distance(transform.position, _target.transform.position) <= _attackRadius)
            {
                _target.Damage();
                _target = null;
            }

            _animator.Play(Animator.StringToHash("Idle"));
            _attackInProgress = false;
        }
    }
}