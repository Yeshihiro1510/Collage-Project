using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _root;
    
    public float Sensitivity;
    public Vector2 SensitivityOffset;
    public Vector2 VerticalRange;
    public float Smoothness;
    public float ZoomSensitivity;
    public Vector2 ZoomRange;
    public float ZoomSmoothness;
    
    private Vector2 _rotation;
    private float _zoom;
    private bool _isLocked;

    private void Start()
    {
        _zoom = _camera.fieldOfView;
    }

    private void Update()
    {
        _root.transform.localRotation = Quaternion.Lerp(
            _root.transform.localRotation,
            Quaternion.Euler(0f, _rotation.x, 0f),
            Smoothness * Time.deltaTime);
        _camera.transform.localRotation = Quaternion.Lerp(
            _camera.transform.localRotation,
            Quaternion.Euler(_rotation.y, 0, 0),
            Smoothness * Time.deltaTime);
        _camera.fieldOfView = Mathf.Lerp(_camera.fieldOfView, _zoom, ZoomSmoothness * Time.deltaTime);
    }

    public void Rotate(Vector2 velocity)
    {
        if (!_isLocked) return;
        _rotation.x += velocity.x * Sensitivity * SensitivityOffset.x;
        _rotation.y -= velocity.y * Sensitivity * SensitivityOffset.y;
        _rotation.y = Mathf.Clamp(_rotation.y, VerticalRange.x, VerticalRange.y);
    }

    public void Zoom(Vector2 scroll)
    {
        if (!_isLocked) return;
        _zoom = Mathf.Clamp(_zoom - scroll.y * ZoomSensitivity, ZoomRange.x, ZoomRange.y);
    }

    public void LockCursor()
    {
        Cursor.lockState = Cursor.lockState == CursorLockMode.Locked ? CursorLockMode.None : CursorLockMode.Locked;
        _isLocked = Cursor.lockState == CursorLockMode.Locked;
    }
}