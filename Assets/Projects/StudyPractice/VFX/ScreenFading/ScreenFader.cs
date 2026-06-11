using DG.Tweening;
using UnityEngine;

public class ScreenFader : MonoBehaviour
{
    [SerializeField] private Material _material;

    [Header("Defaults")]
    [SerializeField] private float _duration;
    [SerializeField] private Ease _inEase;
    [SerializeField] private Ease _outEase;
    [SerializeField] private bool _inverseBtw;

    private Tweener _in;
    private Tweener _out;

    public float Progress
    {
        get => _material.GetFloat("_Progress");
        set => _material.SetFloat("_Progress", value);
    }

    private void Awake()
    {
        Progress = 0;
    }

    private void OnDestroy()
    {
        Progress = 0;
    }

    public Tweener In()
    {
        Kill();
        _in = DOVirtual.Float(0f, 1f, _duration, v => Progress = v).SetEase(_inEase);
        return _in;
    }

    public Tweener Out()
    {
        Kill();
        _out = DOVirtual.Float(1f, 0f, _duration, v => Progress = v).SetEase(_outEase);
        
        if (_inverseBtw)
        {
            _material.SetFloat("_Inverse", 1 - _material.GetFloat("_Inverse"));
            _out.onComplete = () => _material.SetFloat("_Inverse", 1 - _material.GetFloat("_Inverse"));
        }

        return _out;
    }

    public void Kill()
    {
        _in?.Kill();
        _out?.Kill();
    }
}