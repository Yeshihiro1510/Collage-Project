using Projects.StudyPractice.Root;

namespace Projects.StudyPractice.MainMenu
{
    public class SettingsController
    {
        public SettingsController(SettingsView view, AudioController audioController)
        {
            _view = view;
            _audioController = audioController;
            view.GeneralSlider.onValueChanged.AddListener(_ => Apply());
            view.MusicSlider.onValueChanged.AddListener(_ => Apply());
            view.SFXSlider.onValueChanged.AddListener(_ => Apply());
        }
        
        private readonly SettingsView _view;
        private readonly AudioController _audioController;

        private void Apply()
        {
            var data = new VolumeData(_view.GeneralSlider.value, _view.MusicSlider.value, _view.SFXSlider.value);
            _audioController.Apply(data);
        }
    }
}