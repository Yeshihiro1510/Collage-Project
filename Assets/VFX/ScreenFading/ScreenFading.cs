using DG.Tweening;
using UnityEngine;

public class ScreenFading : MonoBehaviour
{
    [SerializeField] private Material _material;

    [field: SerializeField] public float Duration { get; set; }
    [field: SerializeField] public Ease InEase { get; set; }
    [field: SerializeField] public Ease OutEase { get; set; }

    public Tweener In()
    {
        return DOVirtual.Float(1f, 0f, Duration, v => _material.SetFloat("_Progress", v)).SetEase(OutEase);
    }

    public Tweener Out()
    {
        return DOVirtual.Float(0f, 1f, Duration, v => _material.SetFloat("_Progress", v)).SetEase(InEase);
    }
}