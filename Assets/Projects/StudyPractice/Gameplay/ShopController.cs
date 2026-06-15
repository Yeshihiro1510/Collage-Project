using Projects.InventorySystem__Legacy_.Source;
using Projects.StudyPractice.Root;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Projects.StudyPractice.Gameplay
{
    public class ShopController
    {
        public ShopController(ShopView view, ShopSystem system, MoneyModel money, InventoryModel inventory)
        {
            view.Initialize();
            system.updated += OnUpdated;
            var routine = Coroutines.Run(system.Routine());
            SceneManager.activeSceneChanged += ActiveSceneChanged;

            return;

            void OnUpdated(ItemData[] data)
            {
                view.Clear();
                foreach (var itemData in data)
                {
                    view.Draw(itemData.Name, itemData.Commentary + $"\nPrice: {itemData.Price}", itemData.Icon, () =>
                    {
                        if (money.TryGet(itemData.Price))
                        {
                            inventory.AddItemToAnyBaseSlot(itemData, 1);
                        }
                    });
                }
            }

            void ActiveSceneChanged(Scene arg0, Scene arg1)
            {
                Coroutines.Stop(routine);
            }
        }
    }
}