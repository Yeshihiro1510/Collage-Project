using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Movement _movement;
    [SerializeField] private CameraController _cameraController;
    [SerializeField] private Button _openMenuButton;
    [SerializeField] private TMP_Text _openMenuText;
    private InputSystem_Actions _inputSystem;
    
    private void Awake()
    {
        _inputSystem = new InputSystem_Actions();
        _inputSystem.Player.Move.performed += ctx => _movement.Move(ctx.ReadValue<Vector2>());
        _inputSystem.Player.Move.canceled += _ => _movement.Move(Vector2.zero);
        _inputSystem.Player.Look.performed += ctx => _cameraController.Rotate(ctx.ReadValue<Vector2>());
        _inputSystem.Player.Zoom.performed += ctx =>  _cameraController.Zoom(ctx.ReadValue<Vector2>());
        _inputSystem.Player.Cursor.performed += _ => _cameraController.LockCursor();
        _openMenuButton.onClick.AddListener(SwitchMenu);
    }

    private void Start()
    {
        CloseMenu();
        _cameraController.LockCursor();
    }
    
    private void SwitchMenu()
    {
        switch (_inputSystem.Player.enabled)
        {
            case true when !_inputSystem.UI.enabled: OpenMenu(); break;
            case false when _inputSystem.UI.enabled:
            default: CloseMenu(); break;
        }
    }

    private void OpenMenu()
    {
        _inputSystem.Player.Disable();
        _inputSystem.UI.Enable();
        _openMenuText.text = "Close menu";
    }

    private void CloseMenu()
    {
        _inputSystem.Player.Enable();
        _inputSystem.UI.Disable();
        _openMenuText.text = "Open menu";
    }
}