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

        private bool _isOpen;

        private void Awake()
        {
            gameObject.SetActive(false);
        }

        public void Toggle()
        {
            if (_isOpen) Close();
            else Open();
        }

        public void Open()
        {
            ClearAnimations();
            gameObject.SetActive(true);
            transform.DORotate(Vector3.zero, 0.5f, RotateMode.FastBeyond360).From(Vector3.forward * -360).SetEase(Ease.OutCubic);
            _alphaGroup.DOFade(1f, 0.5f).From(0f).SetEase(Ease.OutCubic);
            _isOpen = true;
        }

        public void Close()
        {
            ClearAnimations();
            transform.DORotate(Vector3.forward * -360, 0.5f, RotateMode.FastBeyond360).SetEase(Ease.OutCubic);
            _alphaGroup.DOFade(0f, 0.5f)
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