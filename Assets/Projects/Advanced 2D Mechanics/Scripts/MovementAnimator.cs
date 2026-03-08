using UnityEngine;

namespace Projects.Advanced_2D_Mechanics
{
    public class MovementAnimator : MonoBehaviour
    {
        [SerializeField] private TopDownMovement _movement;
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;

        private static readonly int IsMoving = Animator.StringToHash("IsMoving");
        
        private float _lastXDirection;

        private void OnEnable()
        {
            _movement.onMove += OnMove;
            _movement.onStop += OnStop;
        }

        private void OnDisable()
        {
            _movement.onMove -= OnMove;
            _movement.onStop -= OnStop;
        }

        private void OnMove(Vector2 direction)
        {
            _animator.SetBool(IsMoving, true);
            _lastXDirection = direction.x;
            if (direction.x > 0) _spriteRenderer.flipX = false;
            else if (direction.x < 0) _spriteRenderer.flipX = true;
        }

        private void OnStop()
        {
            _animator.SetBool(IsMoving, false);
        }
    }
}