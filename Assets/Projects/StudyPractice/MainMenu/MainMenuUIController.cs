using UnityEngine;

namespace Projects.StudyPractice.MainMenu
{
    public class MainMenuUIController
    {
        public MainMenuUIController(MainMenuUIView UI)
        {
            var settingsView = Object.Instantiate(Resources.Load<SettingsView>("SettingsWindow"), UI.transform);
            var settingsController = new SettingsController(settingsView, Root.Root.Instance.AudioController);
            settingsView.Initialize(new Vector2(Screen.width / 2 + 250, Screen.height / 2));

            UI.PlayButton.onClick.AddListener(Root.Root.Instance.LoadGameplay);
            UI.SettingsButton.onClick.AddListener(() => settingsView.Toggle());
            UI.ExitButton.onClick.AddListener(Application.Quit);
            
            settingsView.CloseButton.onClick.AddListener(settingsView.Close);
        }
    }
}