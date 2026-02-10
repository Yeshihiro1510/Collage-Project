using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Projects.TilemapProject
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlatformerMovement : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _foots;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;
        private InputSystem_Actions _inputSystem;
        private Rigidbody2D _rigidbody;
        private float _direction;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _inputSystem = new InputSystem_Actions();
            _inputSystem.Player.Move.performed += OnMove;
            _inputSystem.Player.Move.canceled += OnStop;
            _inputSystem.Player.Jump.performed += OnJump;
            _inputSystem.Enable();
        }

        private void OnDrawGizmos()
        {
            if (_foots != null)
            {
                Gizmos.DrawLine(_foots.position, _foots.position + Vector3.down * _groundCheckRadius);
            }
        }

        private void Update()
        {
            _rigidbody.linearVelocityX = _direction * _speed;
        }

        private void OnJump(InputAction.CallbackContext obj)
        {
            if (IsGrounded())
            {
                _rigidbody.linearVelocityY = _jumpForce;
            }
        }

        private void OnStop(InputAction.CallbackContext obj)
        {
            _direction = 0;
            _animator.SetFloat("Velocity", 0);
        }

        private void OnMove(InputAction.CallbackContext obj)
        {
            _direction = obj.ReadValue<Vector2>().x;
            _animator.SetFloat("Direction", _direction);
            _animator.SetFloat("Velocity", 1);
        }

        private bool IsGrounded()
        {
            return Physics2D.RaycastAll(_foots.position, Vector2.down, _groundCheckRadius).Any(r => r.collider.gameObject != gameObject);
        }
    }
}