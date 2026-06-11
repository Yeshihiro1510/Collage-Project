using Projects.InventorySystem.Source;
using Projects.StudyPractice.Root;
using UnityEngine;

namespace Projects.StudyPractice.Gameplay
{
    public class GameplayEntryPoint : MonoBehaviour, IEntryPoint
    {
        public void Init()
        {
            var UI = Instantiate(Resources.Load<GameplayUIView>("GameplayUI"));
            Root.Root.Instance.RootUI.SetSceneUI(UI.transform);
            var inventoryController = new InventoryController(UI.InventoryView);
        }
    }
}