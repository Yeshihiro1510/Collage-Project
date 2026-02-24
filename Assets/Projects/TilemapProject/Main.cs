using Projects.TilemapProject;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Main : MonoBehaviour
{
    [SerializeField] private float _deathHigh;
    private ScreenFading _fader;
    private Transform _player;
    private bool _isDead;

    private void Awake()
    {
        _fader = FindAnyObjectByType<ScreenFading>();
        _player = FindAnyObjectByType<PlatformerController>().transform;
    }

    private void Start()
    {
        _fader.Out();
    }

    private void Update()
    {
        if (_player.position.y < _deathHigh && !_isDead)
        {
            _isDead = true;
            _fader.In().onComplete += () => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}