using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Projects.DOTweenProject
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private Transform _mesh;
        public float RotationDuration;
        public float JumpRange;
        public float JumpForce;
        public int NumJumps;
        public float JumpDuration;

        private InputSystem_Actions _inputSystem;
        private float _direction;

        private void Awake()
        {
            _inputSystem = new InputSystem_Actions();
            _inputSystem.Player.Enable();
            _inputSystem.Player.Move.performed += Move;
            _inputSystem.Player.Jump.performed += Jump;
        }

        private void OnDestroy()
        {
            _inputSystem.Player.Move.performed -= Move;
            _inputSystem.Player.Jump.performed -= Jump;
        }

        private void Move(InputAction.CallbackContext context)
        {
            _direction = context.ReadValue<Vector2>().x;
            _mesh.DORotate(Vector3.up * (_direction * 90f), RotationDuration);
        }

        private void Jump(InputAction.CallbackContext context)
        {
            _mesh.DOJump(new Vector3(_mesh.position.x + _direction * JumpRange, 0, 0), JumpForce, NumJumps,
                JumpDuration);
        }
    }
}