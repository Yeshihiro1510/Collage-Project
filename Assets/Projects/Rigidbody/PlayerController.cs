using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float jumpForce = 10f;

    [SerializeField] private TMP_Text _healthText;

    private Rigidbody2D rb;
    private bool isGrounded = false;

    private int _health = 3;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        UpdateHealthText();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");
        if (move != 0) rb.linearVelocityX = move * moveSpeed;

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground")) isGrounded = false;
    }

    public void DealDamage(int damage)
    {
        _health -= Mathf.Abs(damage);
        UpdateHealthText();
        if (_health <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateHealthText() => _healthText.text = $"Health: {_health}";
}