using System.Linq;
using UnityEngine;
using UnityEngine.Events;

namespace Projects.Advanced_3D
{
    public class Button : MonoBehaviour
    {
        public UnityEvent<GameObject> OnCollision;
        public UnityEvent<GameObject> OnExitCollision;
        public UnityEvent<GameObject> OnTrigger;
        public UnityEvent<GameObject> OnExitTrigger;
        
        [SerializeField] private GameObject[] _compareGameObject;
        
        private void OnCollisionEnter(Collision other)
        {
            if (_compareGameObject.Any(g => g == other.gameObject))
                OnCollision?.Invoke(other.gameObject);
        }

        private void OnCollisionExit(Collision other)
        {
            if (_compareGameObject.Any(g => g == other.gameObject))
                OnExitCollision?.Invoke(other.gameObject);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (_compareGameObject.Any(g => g == other.gameObject))
                OnTrigger?.Invoke(other.gameObject);
        }

        private void OnTriggerExit(Collider other)
        {
            if (_compareGameObject.Any(g => g == other.gameObject))
                OnExitTrigger?.Invoke(other.gameObject);
        }
    }
}