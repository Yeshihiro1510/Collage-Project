using Projects.InventorySystem__Legacy_.Source;
using Projects.StudyPractice.Root;
using UnityEngine.SceneManagement;

namespace Projects.StudyPractice.Gameplay
{
    public class ShopController
    {
        public ShopController(ShopView view, ShopSystem system)
        {
            view.Initialize();
            system.updated += OnUpdated;
            var routine = Coroutines.Run(system.Routine());
            SceneManager.activeSceneChanged += ActiveSceneChanged;
            
            return;

            void OnUpdated(ItemData[] data)
            {
                view.Clear();
                view.Draw(data);
            }

            void ActiveSceneChanged(Scene arg0, Scene arg1)
            {
                Coroutines.Stop(routine);
            }
        }
    }
}