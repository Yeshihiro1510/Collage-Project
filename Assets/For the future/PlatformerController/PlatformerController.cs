using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Projects.TilemapProject
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlatformerController : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _foots;
        [SerializeField] private Rigidbody2D _rigidbody;
        
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private float _speed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _coyoteTime;
        [SerializeField] private int _jumpLimit;
        
        private InputSystem_Actions _inputSystem;
        private float _direction;
        private float _lastGroundedTime;
        private int _jumps;
        
        public bool IsGrounded => Physics2D
            .RaycastAll(_foots.position, Vector2.down, _groundCheckRadius)
            .Any(r => r.collider.gameObject != gameObject);
        public bool IsCoyoteTime => Time.time - _lastGroundedTime <= _coyoteTime;
        public bool IsAvailableJumps => _jumps < _jumpLimit;

        private void Awake()
        {
            if (_rigidbody == null) _rigidbody = GetComponent<Rigidbody2D>();
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

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (IsGrounded)
            {
                _jumps = 0;
            }
        }

        private void OnCollisionStay2D(Collision2D other)
        {
            if (IsGrounded)
            {
                _lastGroundedTime = Time.time;
            }
        }

        private void OnJump(InputAction.CallbackContext obj)
        {
            if (IsCoyoteTime && IsAvailableJumps)
            {
                _rigidbody.linearVelocityY = _jumpForce;
                _jumps++;
                _animator.SetTrigger("Jump");
            }
        }

        private void OnStop(InputAction.CallbackContext obj)
        {
            _direction = 0;
            _animator.SetFloat("VelocityX", 0);
        }

        private void OnMove(InputAction.CallbackContext obj)
        {
            _direction = obj.ReadValue<Vector2>().x;
            _animator.SetFloat("VelocityX", _direction);
            _animator.SetFloat("LastDirection", _direction);
        }
    }
}