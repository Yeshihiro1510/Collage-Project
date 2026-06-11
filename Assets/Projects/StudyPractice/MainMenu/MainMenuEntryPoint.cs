using Projects.StudyPractice.Root;
using UnityEngine;

namespace Projects.StudyPractice.MainMenu
{
    public class MainMenuEntryPoint : MonoBehaviour, IEntryPoint
    {
        public void Init()
        {
            var UI = Instantiate(Resources.Load<MainMenuUIView>("MainMenuUI"));
            Root.Root.Instance.RootUI.SetSceneUI(UI.transform);
            var controller = new MainMenuUIController(UI);
        }
    }
}