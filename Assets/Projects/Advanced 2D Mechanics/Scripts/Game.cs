using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Projects.Advanced_2D_Mechanics
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private ScreenFading _screenFading;
        [SerializeField] private Killable _player;

        private void OnEnable()
        {
            _player.onDeath += Reload;
        }

        private void OnDisable()
        {
            _player.onDeath -= Reload;
        }

        private void Start()
        {
            _screenFading.Out();
        }

        public void Reload()
        {
            _screenFading.In().OnComplete(() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex));
        }
    }
}