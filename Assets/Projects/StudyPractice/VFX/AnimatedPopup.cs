using DG.Tweening;
using UnityEngine;

namespace Projects.StudyPractice.VFX
{
    public class AnimatedPopup : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _alphaGroup;
        private Vector2 _spawnPoint;
        private bool _isOpen;

        public virtual void Initialize(Vector2 spawnPoint)
        {
            _spawnPoint = spawnPoint;
            gameObject.SetActive(false);
        }

        public void Toggle()
        {
            if (_isOpen) CloseUnsafe();
            else OpenUnsafe();
        }

        public void Open()
        {
            if (!_isOpen) OpenUnsafe();
        }

        public void Close()
        {
            if (_isOpen) CloseUnsafe();
        }

        protected void OpenUnsafe()
        {
            ClearAnimations();
            gameObject.SetActive(true);
            transform.position = _spawnPoint;
            transform.DOScale(Vector3.one, 0.6f).From(Vector3.zero).SetEase(Ease.OutCubic);
            transform.DORotate(Vector3.zero, 0.6f, RotateMode.FastBeyond360).From(Vector3.forward * -270)
                .SetEase(Ease.OutCubic);
            _alphaGroup.DOFade(1f, 0.3f).From(0f).SetEase(Ease.OutCubic);
            _isOpen = true;
        }

        protected void CloseUnsafe()
        {
            ClearAnimations();
            transform.DOScale(Vector3.zero, 0.6f).SetEase(Ease.OutCubic);
            transform.DORotate(Vector3.forward * -270, 0.6f, RotateMode.FastBeyond360).SetEase(Ease.OutCubic);
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