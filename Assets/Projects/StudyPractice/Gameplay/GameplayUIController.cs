using Projects.InventorySystem__Legacy_.Source;
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
            pauseMenu.Initialize(pauseMenu.transform.position.y, 0f);

            var notificationsView = Object.Instantiate(Resources.Load<NotificationView>("NotificationView"), UI.transform);
            var notificationController = new NotificationController(notificationsView, Root.Root.Instance.AudioController);
            notificationsView.Initialize(15f, -notificationsView.GetComponent<RectTransform>().rect.height);
            
            var inventoryView = Object.Instantiate(Resources.Load<InventoryViewLegacy>("InventoryView"), UI.transform);
            inventoryView.Initialize();
            var inventoryControllerLegacy = new InventoryControllerLegacy(inventoryView);

            UI.PauseButton.onClick.AddListener(() =>
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