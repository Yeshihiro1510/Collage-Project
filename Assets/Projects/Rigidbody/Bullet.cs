using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
        if (collision.gameObject.TryGetComponent<Enemy>(out var enemy)) enemy.DisableDamage();
    }
}