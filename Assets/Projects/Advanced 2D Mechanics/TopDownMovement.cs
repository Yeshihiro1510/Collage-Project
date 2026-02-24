using System;
using UnityEngine;

namespace Projects.Advanced_2D_Mechanics
{
    public class TopDownMovement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody2D;
        [SerializeField] private float _speed;
        
        public event Action<Vector2> onMove;
        public event Action onStop;

        public void Move(Vector2 direction)
        {
            _rigidbody2D.linearVelocity = direction.normalized * _speed;
            if (direction != Vector2.zero) onMove?.Invoke(direction);
            else onStop?.Invoke();
        }
    }
}