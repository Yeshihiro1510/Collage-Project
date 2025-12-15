using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private float _density;
    private readonly List<Rigidbody2D> _toPush = new();

    private void Update()
    {
        foreach (var rb in _toPush)
        {
            rb.AddForce(Vector2.up * _density);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Rigidbody2D rb)) _toPush.Add(rb);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Rigidbody2D rb)) _toPush.Remove(rb);
    }
}