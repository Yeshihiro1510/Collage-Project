using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    public float seconds;
    public UnityEvent OnTimerEnd;
    
    /// <summary>
    /// first item - current time |
    /// second item - goal time
    /// </summary>
    public UnityEvent<(float, float)> OnTick;

    private Coroutine _coroutine;

    public void Run() => _coroutine ??= StartCoroutine(Work());

    private IEnumerator Work()
    {
        var goalTime = Mathf.Abs(seconds);
        var time = 0f;

        while (time < goalTime)
        {
            time += Time.deltaTime;
            OnTick?.Invoke((time, goalTime));
            yield return new WaitForEndOfFrame();
        }

        _coroutine = null;
        OnTimerEnd?.Invoke();
    }
}