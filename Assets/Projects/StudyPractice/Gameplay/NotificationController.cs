using Projects.InventorySystem__Legacy_.Source;
using Projects.StudyPractice.Root;
using Projects.Utils;
using UnityEngine;

namespace Projects.StudyPractice.Gameplay
{
    public class NotificationController
    {
        public static string Path => Application.persistentDataPath + "/notifications_data.json";

        public NotificationController(NotificationView view, AudioController audioController, ShopSystem shopSystem)
        {
            view.Initialize(15f, -view.GetComponent<RectTransform>().rect.height);
            shopSystem.updated += Updated;
            return;

            void Updated(ItemData[] data)
            {
                if (!JsonSavingUtil.TryGet<NotificationsData>(Path, out var result) || result.hide) return;
                view.Push("New shop items available!!!", "Touch here to watch whats new youll see", data[0].Icon);
                audioController.Play("message");
            }
        }
    }
}