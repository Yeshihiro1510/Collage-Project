using Projects.InventorySystem__Legacy_.Source;
using UnityEngine;

namespace Projects.StudyPractice.Gameplay
{
    public class GameplayUIController
    {
        public GameplayUIController(GameplayUIView UI)
        {
            var inventoryView = Object.Instantiate(Resources.Load<InventoryView>("InventoryView"), UI.Content, false)
                .InventoryWindow;
            var inventoryControllerLegacy = new InventoryController(inventoryView);

            var shopWindowView = Object.Instantiate(Resources.Load<ShopView>("ShopWindow"), UI.Content, false);
            var shopSystem = new ShopSystem();
            var shopController = new ShopController(shopWindowView, shopSystem);

            var settingsView = Object.Instantiate(Resources.Load<SettingsView>("SettingsWindow"), UI.Content, false);
            var settingsController = new SettingsController(settingsView, Root.Root.Instance.AudioController);

            var pauseMenu = Object.Instantiate(Resources.Load<PauseMenuView>("PauseMenu"), UI.Content, false);
            var pauseController = new PauseMenuController(UI, pauseMenu, settingsView);

            var notificationsView =
                Object.Instantiate(Resources.Load<NotificationView>("NotificationView"), UI.Content, false);
            var notificationController =
                new NotificationController(notificationsView, Root.Root.Instance.AudioController, shopSystem);

            var timerController = new TimerController(UI.Timer, shopSystem);

            var moneyModel = new MoneyModel(0);
            var moneyController = new MoneyController(UI.MoneyMenu, moneyModel);

            UI.PauseButton.onClick.AddListener(() =>
            {
                settingsView.Close();
                inventoryView.Close();
                shopWindowView.Close();
                pauseMenu.Toggle();
            });
            UI.InventoryButton.onClick.AddListener(() =>
            {
                pauseMenu.Close();
                settingsView.Close();
                inventoryView.Toggle();
            });
            UI.ShopButton.onClick.AddListener(() =>
            {
                pauseMenu.Close();
                settingsView.Close();
                shopWindowView.Toggle();
            });
            settingsView.CloseButton.onClick.AddListener(() =>
            {
                settingsView.Close();
                pauseMenu.Open();
            });
            notificationsView.onClick += () =>
            {
                settingsView.Close();
                pauseMenu.Close();
                notificationsView.Close();
                shopWindowView.Open();
            };
        }
    }
}