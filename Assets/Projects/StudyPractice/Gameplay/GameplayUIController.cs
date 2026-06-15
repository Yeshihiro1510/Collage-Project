using UnityEngine;

namespace Projects.StudyPractice.Gameplay
{
    public class GameplayUIController
    {
        public GameplayUIController(GameplayUIView UI)
        {
            var settingsView = Object.Instantiate(Resources.Load<SettingsView>("SettingsWindow"), UI.transform);
            var settingsController = new SettingsController(settingsView, Root.Root.Instance.AudioController);
            settingsView.Initialize(new Vector2(Screen.width / 2, Screen.height / 2));
            
            var pauseMenu = Object.Instantiate(Resources.Load<PauseMenuView>("PauseMenu"), UI.transform, false);
            var pauseController = new PauseMenuController(UI, pauseMenu, settingsView);
            pauseMenu.Initialize(pauseMenu.transform.position.y);

            UI.PauseButton.onClick.AddListener(()=>
            {
                settingsView.Close();
                pauseMenu.Toggle();
            });
            settingsView.CloseButton.onClick.AddListener(() =>
            {
                settingsView.Close();
                pauseMenu.Open();
            });
        }
    }
}