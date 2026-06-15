using UnityEngine;

namespace Projects.StudyPractice.MainMenu
{
    public class MainMenuUIController
    {
        public MainMenuUIController(MainMenuUIView UI)
        {
            UI.PlayButton.onClick.AddListener(Root.Root.Instance.LoadGameplay);
            UI.SettingsButton.onClick.AddListener(() =>
            {
                if (UI.Settings == null)
                {
                    UI.Settings = Object.Instantiate(Resources.Load<SettingsView>("SettingsPopup"), UI.transform);
                    var settingsController = new SettingsController(UI.Settings, Root.Root.Instance.AudioController);
                }

                UI.Settings.Toggle();
            });
            UI.ExitButton.onClick.AddListener(Application.Quit);
        }
    }
}