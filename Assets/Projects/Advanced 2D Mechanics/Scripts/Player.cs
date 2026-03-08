using UnityEngine;
using UnityEngine.InputSystem;

namespace Projects.Advanced_2D_Mechanics
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private TopDownMovement _movement;
        [SerializeField] private Shooter _shooter;
        [SerializeField] private CameraController _cameraController;
        
        [SerializeField] private float _length;
        
        private InputSystem_Actions _inputSystem;

        private void Awake()
        {
            _inputSystem = new InputSystem_Actions();
        }

        private void OnEnable()
        {
            _inputSystem.Player.Move.performed += OnMove;
            _inputSystem.Player.Move.canceled += OnStop;
            _inputSystem.Player.LMB.performed += OnShoot;

            _inputSystem.Enable();
        }


        private void OnDisable()
        {
            _inputSystem.Player.Move.performed -= OnMove;
            _inputSystem.Player.Move.canceled -= OnStop;

            _inputSystem.Disable();
        }

        private void Update()
        {
            _cameraController.MoveTo(transform.position, _movement.Direction);
        }

        private void OnMove(InputAction.CallbackContext context) => _movement.Move(context.ReadValue<Vector2>());
        private void OnStop(InputAction.CallbackContext context) => _movement.Stop();

        private void OnShoot(InputAction.CallbackContext context)
        {
            var mousePos = _cameraController.Camera.ScreenToWorldPoint(Input.mousePosition);
            var direction = (mousePos - transform.position).normalized * _length;
            _shooter.Shoot(direction);
        }
    }
}
