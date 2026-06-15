using UnityEngine;

namespace Projects.StudyPractice.MainMenu
{
    public class MainMenuUIController
    {
        public MainMenuUIController(MainMenuUIView UI)
        {
            var settingsWindow = Object.Instantiate(Resources.Load<SettingsView>("SettingsWindow"), UI.transform);
            var settingsController = new SettingsController(settingsWindow, Root.Root.Instance.AudioController);
            settingsWindow.Initialize(new Vector2(Screen.width / 2 + 250, Screen.height / 2));

            UI.PlayButton.onClick.AddListener(Root.Root.Instance.LoadGameplay);
            UI.SettingsButton.onClick.AddListener(() => settingsWindow.Toggle());
            UI.ExitButton.onClick.AddListener(Application.Quit);
        }
    }
}