using System.Collections;
using UnityEngine;

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
                }

                return _instance;

                void OnQuit()
                {
                    _instance = null;
                    Application.quitting -= OnQuit;
                }
            }
        }
        private static Coroutines _instance;
        public static Coroutine Run(IEnumerator routine) => Instance.StartCoroutine(routine);
        public static void Stop(Coroutine routine) => Instance.StopCoroutine(routine);
    }
}