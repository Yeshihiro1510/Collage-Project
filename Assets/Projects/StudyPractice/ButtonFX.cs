using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Projects.StudyPractice
{
    public class ButtonFX : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        [SerializeField] private float _rotationSpeed = 2f;
        [SerializeField] private float _scaleSpeed = 3f;
        [SerializeField] private float _rotationValue = 3f;
        [SerializeField] private float _scaleMultiplier = 1.1f;
        [SerializeField] private float _punchForce = .25f;
        private bool _entered;
        private bool _clicked;

        private void Update()
        {
            transform.localRotation = Quaternion.Slerp(transform.localRotation, _entered
                ? Quaternion.Euler(0, 0, Mathf.Sin(Time.time * _rotationSpeed) * _rotationValue)
                : Quaternion.identity, Time.deltaTime * _rotationSpeed);
            
            if (!_clicked)
            {
                transform.localScale = Vector3.Lerp(transform.localScale,
                    _entered ? Vector3.one * _scaleMultiplier : Vector3.one,
                    Time.deltaTime * _scaleSpeed);
            }
        }

        public void OnPointerEnter(PointerEventData eventData) => _entered = true;
        public void OnPointerExit(PointerEventData eventData) => _entered = false;

        public void OnPointerClick(PointerEventData eventData)
        {
            _clicked = true;
            transform.DOPunchScale(-Vector3.one * _punchForce, .3f, 0, 0f).OnComplete(() => _clicked = false);
        }
    }
}