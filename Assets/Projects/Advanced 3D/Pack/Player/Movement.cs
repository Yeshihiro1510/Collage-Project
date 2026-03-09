using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Projects.Advanced_3D
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        [SerializeField] private float _linearSpeed;
        [SerializeField] private float _angularSpeed;
        [SerializeField] private float _jumpForce;
        [SerializeField] private Vector3 _groundCheckPosition;
        [SerializeField] private float _groundCheckRadius;
        [SerializeField] private float _deathHeight;

        private InputSystem_Actions _inputSystem;
        private Vector3 _moveInput;
        private Vector3 _rotationInput;

        private void OnDrawGizmos()
        {
            Gizmos.DrawLine(transform.position + _groundCheckPosition,
                transform.position + _groundCheckPosition + Vector3.down * _groundCheckRadius);
        }

        private void Awake()
        {
            _inputSystem = new InputSystem_Actions();
        }

        private void OnEnable()
        {
            _inputSystem.Player.Move.performed += OnMove;
            _inputSystem.Player.Move.canceled += OnMove;
            _inputSystem.Player.Jump.performed += OnJump;
            _inputSystem.Enable();
        }

        private void OnDisable()
        {
            _inputSystem.Player.Move.performed -= OnMove;
            _inputSystem.Player.Move.canceled -= OnMove;
            _inputSystem.Disable();
        }

        private void Update()
        {
            _rigidbody.MovePosition(_rigidbody.position + _rigidbody.transform.forward * (_moveInput.z * (Time.deltaTime * _linearSpeed)));
            _rigidbody.MoveRotation(Quaternion.Euler(_rigidbody.rotation.eulerAngles + _rotationInput * (Time.deltaTime * (_angularSpeed * 180))));
            if (_rigidbody.position.y <= _deathHeight) Kill();
        }

        private void OnMove(InputAction.CallbackContext context)
        {
            var value = context.ReadValue<Vector2>();
            _moveInput = new Vector3(0, 0, value.y);
            _rotationInput = new Vector3(0, value.x, 0);
        }

        private void OnJump(InputAction.CallbackContext context)
        {
            if (Physics.Raycast(transform.position + _groundCheckPosition, Vector3.down, _groundCheckRadius))
            {
                _rigidbody.AddForce(Vector3.up * _jumpForce, ForceMode.Impulse);
            }
        }

        public void Kill()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}