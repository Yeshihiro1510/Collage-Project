using System;
using System.Collections;
using DG.Tweening;
using Projects.StudyPractice.Gameplay;
using Projects.StudyPractice.MainMenu;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace Projects.StudyPractice.Root
{
    public class Root
    {
        private const string GAMEPLAY_SCENE = "GameplayScene";
        private const string MAIN_MENU_SCENE = "MainMenuScene";
        private const string EMPTY_SCENE = "EmptyScene";

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void Setup()
        {
            Instance = new Root();
        }

        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
        private static void Start()
        {
            Coroutines.Run(Instance.LoadFromAwake<MainMenuEntryPoint>(MAIN_MENU_SCENE, e =>
            {
                e.Init();
                Instance.AudioController.PlayMusic(Resources.Load<AudioClip>("The Stringini Bros - The Human Shields"));
            }));
        }

        private Root()
        {
            RootUI = Object.Instantiate(Resources.Load<RootUIView>("RootUI"));
            RootUI.gameObject.name = "[Root UI]";
            var camera = Object.Instantiate(Resources.Load<Camera>("RootCamera"));
            camera.name = "[Root Camera]";
            AudioController = new AudioController();
            Object.DontDestroyOnLoad(RootUI.gameObject);
            Object.DontDestroyOnLoad(camera.gameObject);
        }

        public static Root Instance;
        public RootUIView RootUI { get; }
        public AudioController AudioController { get; }

        public void LoadMainMenu() => Coroutines.Run(Load<MainMenuEntryPoint>(MAIN_MENU_SCENE));
        public void LoadGameplay() => Coroutines.Run(Load<GameplayEntryPoint>(GAMEPLAY_SCENE));

        private IEnumerator LoadFromAwake<T>(string scene, Action<T> initializer = default) where T : MonoBehaviour, IEntryPoint
        {
            var process = SceneManager.LoadSceneAsync(scene);
            RootUI.ScreenFader.Progress = 1;
            yield return process;
            var entryPoint = Object.FindFirstObjectByType<T>();
            if (initializer == default) entryPoint.Init();
            else initializer(entryPoint);
            yield return RootUI.ScreenFader.Out().WaitForCompletion();
        }

        private IEnumerator Load<T>(string scene, Action<T> initializer = default) where T : MonoBehaviour, IEntryPoint
        {
            var currentScene = SceneManager.GetActiveScene().name;
            yield return SceneManager.LoadSceneAsync(EMPTY_SCENE, LoadSceneMode.Additive);
            yield return RootUI.ScreenFader.In().WaitForCompletion();
            yield return SceneManager.UnloadSceneAsync(currentScene);
            yield return SceneManager.LoadSceneAsync(scene);
            
            var entryPoint = Object.FindFirstObjectByType<T>();
            if (initializer == default) entryPoint.Init();
            else initializer(entryPoint);
            
            yield return RootUI.ScreenFader.Out().WaitForCompletion();
        }
    }
}