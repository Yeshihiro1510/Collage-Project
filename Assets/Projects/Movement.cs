using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Movement : MonoBehaviour
{
    public float Speed = 7f;
    private Rigidbody _rigidbody;
    private Vector2 _input;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (_input != Vector2.zero)
        {
            _rigidbody.MovePosition(_rigidbody.position + (transform.forward * _input.y + transform.right * _input.x) * (Speed * Time.deltaTime));
        }
    }

    public void Move(Vector2 input)
    {
        _input = input;
    }
}
