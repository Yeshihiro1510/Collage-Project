using UnityEngine;

namespace Projects.Advanced_3D
{
    public class PingPong : MonoBehaviour
    {
        [SerializeField] private Transform startPosition, endPosition;
        [SerializeField] private float _speed;

        private void Update()
        {
            transform.position = Vector3.Lerp(startPosition.position, endPosition.position,
                (Mathf.Sin(Time.time * _speed) + 1) / 2);
        }
    }
}