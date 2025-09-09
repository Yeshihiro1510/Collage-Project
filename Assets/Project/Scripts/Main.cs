using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Main : MonoBehaviour
{
    public Transform movePart;
    public Transform[] movePoints;
    public Ease moveEase;
    public float stopTime;
    public float moveTime;
    
    private void Start()
    {
        StartCoroutine(MoveSkeleton());
    }

    private IEnumerator MoveSkeleton()
    {
        var currentPoint = 0;
        yield return new DOTweenCYInstruction.WaitForCompletion(movePart.DOMove(movePoints[currentPoint].position, moveTime).SetEase(moveEase));
        yield return new WaitForSeconds(stopTime);
        
        while (true)
        {
            var nextPoint = currentPoint + 1;
            if (nextPoint == movePoints.Length) nextPoint = 0;
            yield return new DOTweenCYInstruction.WaitForCompletion(movePart.DOMove(movePoints[nextPoint].position, moveTime).SetEase(moveEase));
            currentPoint = nextPoint;
            yield return new WaitForSeconds(stopTime);
        }
    }
}