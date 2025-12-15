using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour
{
    [SerializeField] private TMP_Text _healthText;
    [SerializeField] private int _health = 3;

    void Start()
    {
        UpdateHealthText();
    }

    public void DealDamage(int damage)
    {
        _health -= Mathf.Abs(damage);
        UpdateHealthText();
        if (_health <= 0) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void UpdateHealthText()
    {
        if (_healthText != null) _healthText.text = $"Health: {_health}";
    }
}