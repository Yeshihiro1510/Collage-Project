using System.Collections;
using DG.Tweening;
using UnityEngine;

public class SmasherPlatform : MonoBehaviour
{
    [SerializeField] private Transform _startPoint;
    [SerializeField] private Transform _endPoint;
    [SerializeField] private float _duration;
    [SerializeField] private Ease _ease;
    
    private void Start()
    {
        transform.position = _startPoint.position;
        StartCoroutine(Routine());
    }

    private IEnumerator Routine()
    {
        while (true)
        {
            yield return new DOTweenCYInstruction.WaitForCompletion(transform.DOMove(_endPoint.position, _duration).SetEase(_ease));
            yield return new DOTweenCYInstruction.WaitForCompletion(transform.DOMove(_startPoint.position, _duration).SetEase(_ease));
        }
    }
}
