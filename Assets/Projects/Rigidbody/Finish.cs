using UnityEngine;

public class Finish : MonoBehaviour
{
    [SerializeField] private GameObject _winLabel;

    private void Awake()
    {
        _winLabel.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) Win();
    }

    private void Win()
    {
        _winLabel.SetActive(true);
    }
}