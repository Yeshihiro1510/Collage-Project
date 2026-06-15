using System.Collections;
using Projects.StudyPractice.Root;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Projects.StudyPractice.Gameplay
{
    public class NotificationController
    {
        public NotificationController(NotificationView view, AudioController audioController)
        {
            _view = view;
            _audioController = audioController;
            _routine = Coroutines.Run(Routine());
        }

        private readonly NotificationView _view;
        private readonly AudioController _audioController;
        private readonly Coroutine _routine;

        private IEnumerator Routine()
        {
            SceneManager.activeSceneChanged += SceneChanged;

            while (true)
            {
                yield return new WaitForSeconds(5f);
                _view.Push("Test",
                    "Shvabudabl glab shvabudabl glab shvabudabl glab shvabudabl glab shvabudabl glab shvabudabl glab shvabudabl glab shvabudabl glab shvabudabl glab . . .",
                    null);
                _audioController.Play("message");
                yield return new WaitForSeconds(3f);
                _view.Close();
            }

            void SceneChanged(Scene from, Scene to)
            {
                Coroutines.Stop(_routine);
                SceneManager.activeSceneChanged -= SceneChanged;
            }
        }
    }
}