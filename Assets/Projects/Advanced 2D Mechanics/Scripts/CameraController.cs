using UnityEngine;

namespace Projects.Advanced_2D_Mechanics
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        [SerializeField, Min(0.0001f)] private float _duration;
        [SerializeField, Min(1)] private float _orthographicMin;
        [SerializeField, Min(1)] private float _orthographicMax;
        [SerializeField] private float _directionOffset;
        
        public Camera Camera => _camera;
        
        public Vector2 Position
        {
            get => _camera.transform.position;
            set => _camera.transform.position = new Vector3(value.x, value.y, _camera.transform.position.z);
        }

        public void MoveTo(Vector2 origin, Vector2 direction)
        {
            var goal = origin + direction * _directionOffset;
            var time = Time.deltaTime / _duration;
            
            Position = Vector2.Lerp(Position, goal, time);
            // _camera.orthographicSize = Mathf.Lerp(_orthographicMin, _orthographicMax, progress);
        }
    }
}