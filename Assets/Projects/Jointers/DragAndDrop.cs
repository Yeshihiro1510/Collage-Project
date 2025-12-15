using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody2D))]
public class DragAndDrop : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private Rigidbody2D _rigidbody;
    private Vector2 _offset;
    private bool _isDragging;
    
    private Vector2 MousePosition => Camera.main.ScreenToWorldPoint(Input.mousePosition);
    
    public Vector2 Position => _rigidbody.position;
    public Rigidbody2D Rigidbody => _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!_isDragging) return;
        
        _rigidbody.MovePosition(MousePosition + _offset);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        _isDragging = true;
        _offset = Position - MousePosition;
        _rigidbody.linearVelocity = Vector2.zero;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _isDragging = false;
        _offset = Vector2.zero;
        _rigidbody.linearVelocity = eventData.delta;
    }
}