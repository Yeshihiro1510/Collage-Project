using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ScreenFading : MonoBehaviour
{
    [SerializeField] private Image _image;

    [Header("Defaults")]
    [SerializeField, Range(0f, 1f)] private float _startProgress = 1f;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _inEase;
    [SerializeField] private Ease _outEase;

    public float Progress
    {
        get => _image.material.GetFloat("_Progress");
        set => _image.material.SetFloat("_Progress", value);
    }

    private void Awake()
    {
        Progress = _startProgress;
    }

    public Tweener In()
    {
        return DOVirtual.Float(0f, 1f, _duration, v => Progress = v).SetEase(_inEase);
    }

    public Tweener Out()
    {
        return DOVirtual.Float(1f, 0f, _duration, v => Progress = v).SetEase(_outEase);
    }
}