using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public static class Scenes
{
    public const string GAME = "Game";
}

public class MainMenu : MonoBehaviour
{
    [SerializeField] private CircleFadeEffect _fadeEffect;
    [SerializeField] private Button _button;

    private void Awake()
    {
        _button.onClick.AddListener(() => StartCoroutine(Routine()));
    }

    private void Start()
    {
        _fadeEffect.FadeOut();
    }

    private IEnumerator Routine()
    {
        yield return new DOTweenCYInstruction.WaitForCompletion(_fadeEffect.FadeIn());
        SceneManager.LoadScene(Scenes.GAME);
    }
}