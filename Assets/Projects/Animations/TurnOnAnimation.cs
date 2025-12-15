using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class TurnOnAnimation : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        int hash = Animator.StringToHash("Base Layer.SomeShedAnimation");
        _button.onClick.AddListener(() => _animator.Play(hash));
    }
}