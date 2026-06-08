using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Projects.StudyPractice
{
    public class ButtonsMenuView : MonoBehaviour
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _settingsButton;
        [SerializeField] private Button _exitButton;

        [SerializeField] private SettingsWindowView _settingsWindow;

        private void Awake()
        {
            Initialize();
        }

        public void Initialize()
        {
            _playButton.onClick.AddListener(LoadGameplay);
            _settingsButton.onClick.AddListener(_settingsWindow.Toggle);
            _exitButton.onClick.AddListener(Application.Quit);
        }

        private void LoadGameplay() => StartCoroutine(LoadGameplayRoutine());

        private static IEnumerator LoadGameplayRoutine()
        {
            yield return SceneManager.LoadSceneAsync("Gameplay");
        }
    }
}