using UnityEngine;

namespace Projects.Advanced_2D_Mechanics
{
    [RequireComponent(typeof(Collider2D))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private TopDownMovement _movement;
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        private Player _target;

        private void Awake()
        {
            _movement.onMove += OnMove;
            _movement.onStop += OnStop;
        }

        private void OnDestroy()
        {
            _movement.onMove -= OnMove;
            _movement.onStop -= OnStop;
        }

        private void OnMove(Vector2 direction)
        {
            _animator.SetBool("IsMoving", true);
            _spriteRenderer.flipX = direction.x < 0;
        }

        private void OnStop()
        {
            _animator.SetBool("IsMoving", false);
        }

        private void Update()
        {
            if (_target) _movement.Move(_target.transform.position - _movement.transform.position);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Player target))
                _target = target;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Player target) && _target == target)
            {
                _target = null;
                _movement.Move(Vector2.zero);
            }
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out Player target))
                target.Kill();
        }
    }
}