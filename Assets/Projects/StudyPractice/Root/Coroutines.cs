using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Projects.StudyPractice.Root
{
    public class Coroutines : MonoBehaviour
    {
        private static Coroutines Instance
        {
            get
            {
                if (_instance is null)
                {
                    _instance = new GameObject("[Coroutines]").AddComponent<Coroutines>();
                    DontDestroyOnLoad(_instance.gameObject);
                    Application.quitting += OnQuit;
                    // SceneManager.activeSceneChanged += SceneUnloaded;
                }

                return _instance;

                void OnQuit()
                {
                    _instance = null;
                    Application.quitting -= OnQuit;
                    // SceneManager.activeSceneChanged -= SceneUnloaded;
                }

                // void SceneUnloaded(Scene _, Scene _1)
                // {
                //     Instance.StopAllCoroutines();
                // }
            }
        }


        private static Coroutines _instance;
        public static Coroutine Run(IEnumerator routine) => Instance.StartCoroutine(routine);
        public static void Stop(Coroutine routine) => Instance.StopCoroutine(routine);
    }
}