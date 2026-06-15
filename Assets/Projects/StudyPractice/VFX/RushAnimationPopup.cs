using DG.Tweening;
using UnityEngine;

namespace Projects.StudyPractice.VFX
{
    public abstract class RushAnimationPopup : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _alphaGroup;
        private float _openYPosition;
        private float _closeYPosition;
        protected bool _isOpen;

        public virtual void Initialize(float openYPosition, float closeYPosition)
        {
            _openYPosition = openYPosition;
            _closeYPosition = closeYPosition;
            gameObject.SetActive(false);
        }

        public void Toggle()
        {
            if (_isOpen) CloseUnsafe();
            else OpenUnsafe();
        }
        
        protected void OpenUnsafe()
        {
            ClearAnimations();
            gameObject.SetActive(true);
            transform.DOMoveY(_openYPosition, 0.3f).From(_closeYPosition).SetEase(Ease.OutCubic);
            _alphaGroup.DOFade(1f, 0.3f).From(0f).SetEase(Ease.OutCubic);
            _isOpen = true;
        }

        protected void CloseUnsafe()
        {
            ClearAnimations();
            transform.DOMoveY(_closeYPosition, 0.3f).SetEase(Ease.OutCubic);
            _alphaGroup.DOFade(0f, 0.3f)
                .SetEase(Ease.OutCubic)
                .OnComplete(() => gameObject.SetActive(false));
            _isOpen = false;
        }

        protected void ClearAnimations()
        {
            transform.DOKill();
            _alphaGroup.DOKill();
        }
    }
}