using UnityEngine;

namespace Projects.StudyPractice
{
    public class SettingsView : MonoBehaviour
    {
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