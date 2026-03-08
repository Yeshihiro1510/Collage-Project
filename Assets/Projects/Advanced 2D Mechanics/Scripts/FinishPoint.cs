using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Projects.Advanced_2D_Mechanics
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class FinishPoint : MonoBehaviour
    {
        [SerializeField] private Transform _point;
        [SerializeField] private GameObject _popup;
        [SerializeField] private Button _button;
        [SerializeField] private Game _game;

        private bool _already;
        
        private void Awake()
        {
            _popup.SetActive(false);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(_game.Reload);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(_game.Reload);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Player _))
            {
                if (_already) return;
                _already = true;
                Win();
            }
        }

        private void Win()
        {
            _popup.SetActive(true);
            _popup.transform.DOScale(Vector3.one, 1f).From(Vector3.zero).SetEase(Ease.OutBounce);
        }
    }
}