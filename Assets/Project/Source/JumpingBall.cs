using System.Collections;
using UnityEngine;

public class JumpingBall : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;
    [SerializeField] private Rigidbody2D _rigidbody2D;
    [SerializeField] private float _threshold = 5f;
    [SerializeField] private float _jumpForce = 5f;

    private void Start()
    {
        StartCoroutine(Routine());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _rigidbody2D.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
    }

    private IEnumerator Routine()
    {
        while (true)
        {
            float timeBelowThreshold = 0f;
            do
            {
                float speed = _rigidbody2D.linearVelocity.magnitude;
                if (speed < _threshold)
                {
                    timeBelowThreshold += Time.deltaTime;
                    _spriteRenderer.color = Color.red;
                }
                else
                {
                    _spriteRenderer.color = Color.green;
                }
                yield return null;
            } while (_rigidbody2D.linearVelocity.magnitude < _threshold);

            Debug.Log($"Время ниже порога: {timeBelowThreshold} секунд");
            yield return null;
        }
    }
}