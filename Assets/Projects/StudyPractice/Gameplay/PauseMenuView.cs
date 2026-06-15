using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Projects.StudyPractice.Gameplay
{
    public class PauseMenuView : MonoBehaviour
    {
        [field: SerializeField] public Button ContinueButton { get; private set; }
        [field: SerializeField] public Button SettingsButton { get; private set; }
        [field: SerializeField] public Button BackButton { get; private set; }
        
        [SerializeField] private CanvasGroup _alphaGroup;
        private float _openYPosition;
        private bool _isOpen;

        public void Initialize(float openYPosition)
        {
            _openYPosition = openYPosition;
            gameObject.SetActive(false);
            ContinueButton.onClick.AddListener(Close);
        }
        
        public void Toggle()
        {
            if (_isOpen) Close();
            else Open();
        }
        
        private void Open()
        {
            ClearAnimations();
            gameObject.SetActive(true);
            transform.DOMoveY(_openYPosition, 0.3f).From(0f).SetEase(Ease.OutCubic);
            _alphaGroup.DOFade(1f, 0.3f).From(0f).SetEase(Ease.OutCubic);
            _isOpen = true;
        }

        public void Close()
        {
            ClearAnimations();
            transform.DOMoveY(0f, 0.3f).SetEase(Ease.OutCubic);
            _alphaGroup.DOFade(0f, 0.3f)
                .SetEase(Ease.OutCubic)
                .OnComplete(() => gameObject.SetActive(false));
            _isOpen = false;
        }

        private void ClearAnimations()
        {
            transform.DOKill();
            _alphaGroup.DOKill();
        }
    }
}