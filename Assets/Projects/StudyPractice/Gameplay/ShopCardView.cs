using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Projects.StudyPractice.Gameplay
{
    public class ShopCardView : MonoBehaviour, IPointerClickHandler
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _bodyText;

        public UnityEvent onBuy;

        public void Init(string title, string body, Sprite icon)
        {
            _titleText.text = title;
            _bodyText.text = body;
            _image.sprite = icon;
        }

        public void OnPointerClick(PointerEventData eventData) => onBuy?.Invoke();
    }
}