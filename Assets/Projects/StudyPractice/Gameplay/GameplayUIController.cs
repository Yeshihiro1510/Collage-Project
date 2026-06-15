using UnityEngine;

namespace Projects.StudyPractice.Gameplay
{
    public class GameplayUIController
    {
        public GameplayUIController(GameplayUIView UI)
        {
            var settingsWindow = Object.Instantiate(Resources.Load<SettingsView>("SettingsWindow"), UI.transform);
            var settingsController = new SettingsController(settingsWindow, Root.Root.Instance.AudioController);
            settingsWindow.Initialize(new Vector2(Screen.width / 2, Screen.height / 2));
            
            var pauseMenu = Object.Instantiate(Resources.Load<PauseMenuView>("PauseMenu"), UI.transform, false);
            var pauseController = new PauseMenuController(pauseMenu, settingsWindow);
            pauseMenu.Initialize(pauseMenu.transform.position.y);

            UI.PauseButton.onClick.AddListener(pauseMenu.Toggle);
            UI.PauseButton.onClick.AddListener(settingsWindow.Close);
        }

        private readonly GameplayUIView UI;
    }
}