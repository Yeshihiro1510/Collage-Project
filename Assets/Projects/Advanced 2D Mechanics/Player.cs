using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

namespace Projects.Advanced_2D_Mechanics
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private TopDownMovement _movement;
        [SerializeField] private Animator _animator;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private ScreenFading _screenFading;
        private InputSystem_Actions _inputSystem;
        private bool _isDead;

        private void Awake()
        {
            _inputSystem = new InputSystem_Actions();
            _inputSystem.Enable();

            _inputSystem.Player.Move.performed += Move;
            _inputSystem.Player.Move.canceled += Stop;
            _movement.onMove += OnMove;
            _movement.onStop += OnStop;
        }

        private void Start()
        {
            _screenFading.Out();
        }

        private void OnDestroy()
        {
            _inputSystem.Player.Move.performed -= Move;
            _inputSystem.Player.Move.canceled -= Stop;
            _movement.onMove -= OnMove;
            _movement.onStop -= OnStop;
            _inputSystem.Disable();
        }

        public void Kill()
        {
            if (_isDead) return;
            _isDead = true;
            _screenFading.In().OnComplete(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
        }

        private void Move(InputAction.CallbackContext context) => _movement.Move(context.ReadValue<Vector2>());
        private void Stop(InputAction.CallbackContext _) => _movement.Move(Vector2.zero);

        private void OnMove(Vector2 direction)
        {
            _animator.SetBool("IsMoving", true);
            _spriteRenderer.flipX = direction.x < 0;
        }

        private void OnStop()
        {
            _animator.SetBool("IsMoving", false);
        }
    }
}