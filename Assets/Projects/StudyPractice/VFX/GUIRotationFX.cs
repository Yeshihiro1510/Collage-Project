using UnityEngine;
using UnityEngine.EventSystems;

namespace Projects.StudyPractice
{
    public class GUIRotationFX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private float _rotationSpeed = 2f;
        [SerializeField] private float _rotationValue = 3f;
        private bool _entered;

        private void Update()
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, _entered
                ? Quaternion.Euler(0, 0, Mathf.Sin(Time.time * _rotationSpeed) * _rotationValue)
                : Quaternion.identity, Time.deltaTime * _rotationSpeed);
        }
        
        public void OnPointerEnter(PointerEventData eventData) => _entered = true;
        public void OnPointerExit(PointerEventData eventData) => _entered = false;
    }
}