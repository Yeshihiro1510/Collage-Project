using Projects.StudyPractice.Root;

namespace Projects.StudyPractice
{
    public class SettingsController
    {
        public SettingsController(SettingsView view, AudioController audioController)
        {
            _view = view;
            _audioController = audioController;

            SetView(audioController.VolumeData, false);

            view.GeneralSlider.onValueChanged.AddListener(SliderChanged);
            view.MusicSlider.onValueChanged.AddListener(SliderChanged);
            view.SFXSlider.onValueChanged.AddListener(SliderChanged);
            view.NotificationsToggle.onValueChanged.AddListener(ToggleChanged);
            view.ResetButton.onClick.AddListener(OnReset);

            return;
            void SliderChanged(float _) => OnChange();
            void ToggleChanged(bool _) => OnChange();
        }

        private readonly SettingsView _view;
        private readonly AudioController _audioController;

        private void OnChange()
        {
            var data = new VolumeData(_view.GeneralSlider.value, _view.MusicSlider.value, _view.SFXSlider.value);
            _audioController.Apply(data);
        }

        private void OnReset()
        {
            _audioController.SetDefault();
            SetView(_audioController.VolumeData, false);
        }

        private void SetView(VolumeData volumeData, bool hideNotifications)
        {
            _view.GeneralSlider.SetValueWithoutNotify(volumeData.GeneralVolume);
            _view.MusicSlider.SetValueWithoutNotify(volumeData.MusicVolume);
            _view.SFXSlider.SetValueWithoutNotify(volumeData.SFXVolume);
            _view.NotificationsToggle.SetIsOnWithoutNotify(hideNotifications);
        }
    }
}