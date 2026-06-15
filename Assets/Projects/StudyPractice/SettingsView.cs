using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Projects.StudyPractice
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _alphaGroup;
        [field: SerializeField] public Slider GeneralSlider { get; private set; }
        [field: SerializeField] public Slider MusicSlider { get; private set; }
        [field: SerializeField] public Slider SFXSlider { get; private set; }
        [field: SerializeField] public Toggle NotificationsToggle { get; private set; }
        [field: SerializeField] public Button ResetButton { get; private set; }

        private Vector2 _spawnPoint;
        private bool _isOpen;

        public void Initialize(Vector2 spawnPoint)
        {
            _spawnPoint = spawnPoint;
            gameObject.SetActive(false);
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
            transform.position = _spawnPoint;
            transform.DOScale(Vector3.one, 0.6f).From(Vector3.zero).SetEase(Ease.OutCubic);
            transform.DORotate(Vector3.zero, 0.6f, RotateMode.FastBeyond360).From(Vector3.forward * -270).SetEase(Ease.OutCubic);
            _alphaGroup.DOFade(1f, 0.3f).From(0f).SetEase(Ease.OutCubic);
            _isOpen = true;
        }

        private void Close()
        {
            ClearAnimations();
            transform.DOScale(Vector3.zero, 0.6f).SetEase(Ease.OutCubic);
            transform.DORotate(Vector3.forward * -270, 0.6f, RotateMode.FastBeyond360).SetEase(Ease.OutCubic);
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