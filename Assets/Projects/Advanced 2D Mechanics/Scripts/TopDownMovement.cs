using System;
using UnityEngine;

namespace Projects.Advanced_2D_Mechanics
{
    public class TopDownMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;

        [SerializeField] private float _speed;

        public Vector2 Direction { get; private set; }

        public event Action<Vector2> onMove;
        public event Action onStop;

        public void Update()
        {
            _rigidbody2D.MovePosition(_rigidbody2D.position + Direction * (_speed * Time.deltaTime));
        }

        public void Move(Vector2 direction)
        {
            Direction = direction.normalized;
            onMove?.Invoke(Direction);
        }

        public void Stop()
        {
            Direction = Vector2.zero;
            onStop?.Invoke();
        }
    }
}