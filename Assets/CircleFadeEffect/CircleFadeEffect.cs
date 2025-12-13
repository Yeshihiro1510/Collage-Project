using DG.Tweening;
using UnityEngine;

public class CircleFadeEffect : MonoBehaviour
{
    [SerializeField] private RectTransform _circle;
    [SerializeField] private RectTransform _background;
    [SerializeField] private float _maxScale = 9f;
    private float _progress;

    public float Progress
    {
        get => _progress;
        set
        {
            SetScale(value);
            _progress = value;
        }
    }

    private void Awake()
    {
        Progress = 0;
    }

    public Tweener FadeOut(Ease ease = Ease.InCubic, float duration = 1f)
    {
        return DOVirtual.Float(0, 1, duration, v => Progress = v).SetEase(ease);
    }

    public Tweener FadeIn(Ease ease = Ease.OutCubic, float duration = 1f)
    {
        return DOVirtual.Float(1, 0, duration, v => Progress = v).SetEase(ease);
    }

    private void SetScale(float progress)
    {
        var scale = CalculateScale(progress);
        _circle.localScale = new Vector3(scale, scale);
        _background.localScale = new Vector3(1 / scale, 1 / scale);
    }

    private float CalculateScale(float progress) => Mathf.Clamp(progress, 0.001f, 1f) * _maxScale;
}