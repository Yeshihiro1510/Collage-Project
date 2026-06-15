using UnityEngine;
using UnityEngine.UI;

namespace Projects.StudyPractice.MainMenu
{
    public class MainMenuUIView : MonoBehaviour
    {
        [field: SerializeField] public Button PlayButton { get; private set; }
        [field: SerializeField] public Button SettingsButton { get; private set; }
        [field: SerializeField] public Button ExitButton { get; private set; }
    }
}