using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int _damage = 1;
    private bool _damageEnabled = true;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent<Health>(out var health) && _damageEnabled)
            health.DealDamage(_damage);
    }

    public void DisableDamage() => _damageEnabled = false;
}