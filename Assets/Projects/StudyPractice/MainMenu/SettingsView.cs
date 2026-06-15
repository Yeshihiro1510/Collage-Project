using UnityEngine;
using UnityEngine.UI;

namespace Projects.StudyPractice.MainMenu
{
    public class SettingsView : MonoBehaviour
    {
        [field: SerializeField] public Slider GeneralSlider { get; private set; }
        [field: SerializeField] public Slider MusicSlider { get; private set; }
        [field: SerializeField] public Slider SFXSlider { get; private set; }
        [field: SerializeField] public Toggle NotificationsToggle { get; private set; }

        private void Awake()
        {
            Close();
        }

        public void Toggle()
        {
            gameObject.SetActive(!gameObject.activeInHierarchy);
        }

        public void Open()
        {
            gameObject.SetActive(true);
        }

        public void Close()
        {
            gameObject.SetActive(false);
        }
    }
}