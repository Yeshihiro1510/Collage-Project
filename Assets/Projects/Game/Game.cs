using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private CircleFadeEffect _fadeEffect;
    
    private void Start()
    {
        _fadeEffect.FadeOut();
    }
}