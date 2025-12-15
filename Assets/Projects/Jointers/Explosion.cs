using System.Collections;
using DG.Tweening;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private PointEffector2D _pointEffector;
    [SerializeField] private CircleCollider2D _collider;
    [SerializeField] private SpriteRenderer _renderer;
    [SerializeField] private Sprite _sprite;
    [SerializeField] private float _radius;
    [SerializeField] private float _duration;
    [SerializeField] private float _force;
    [SerializeField] private LayerMask _layers;
    [SerializeField] private Ease _ease;

    private void Start()
    {
        Explode();
    }

    public void Explode()
    {
        StartCoroutine(ExplodeRoutine());
    }

    private IEnumerator ExplodeRoutine()
    {
        _pointEffector.enabled = true;
        _renderer.enabled = true;
        _collider.enabled = true;
        
        _pointEffector.forceMagnitude = _force;
        _pointEffector.colliderMask = _layers;
        _renderer.sprite = _sprite;

        var animation = DOTween.Sequence()
            .Join(DOVirtual.Float(0, _radius, _duration, v => _collider.radius = v))
            .Join(_renderer.transform.DOScale(Vector2.one * (_radius * 2), _duration))
            .SetEase(_ease);

        yield return new DOTweenCYInstruction.WaitForCompletion(animation);
        
        _pointEffector.enabled = false;
        _renderer.enabled = false;
        _collider.enabled = false;
    }
}