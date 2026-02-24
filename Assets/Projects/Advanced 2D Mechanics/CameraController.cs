using UnityEngine;

namespace Projects.Advanced_2D_Mechanics
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        [SerializeField] private float _speed;
        [SerializeField] private Transform _target;

        private void LateUpdate()
        {
            var targetPosition = _target.position;
            targetPosition.z = _camera.transform.position.z;
            _camera.transform.position = Vector3.Lerp(_camera.transform.position, targetPosition, _speed * Time.deltaTime);
        }
    }
}