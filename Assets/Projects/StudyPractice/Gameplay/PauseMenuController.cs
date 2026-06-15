namespace Projects.StudyPractice.Gameplay
{
    public class PauseMenuController
    {
        public PauseMenuController(PauseMenuView view, SettingsView settingsView)
        {
            view.SettingsButton.onClick.AddListener(view.Close);
            view.SettingsButton.onClick.AddListener(settingsView.Toggle);
            view.BackButton.onClick.AddListener(Root.Root.Instance.LoadMainMenu);
        }
    }
}