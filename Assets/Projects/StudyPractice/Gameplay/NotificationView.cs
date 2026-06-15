using System;
using Projects.StudyPractice.VFX;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Projects.StudyPractice.Gameplay
{
    public class NotificationView : RushAnimationPopup, IPointerClickHandler
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _bodyText;
        [SerializeField] private Image _iconImage;

        public event Action onClick;

        public void Push(string title, string body, Sprite icon)
        {
            if (_isOpen) return;
            _titleText.text = title;
            _bodyText.text = body;
            _iconImage.sprite = icon;
            OpenUnsafe();
        }

        public void Close()
        {
            if (_isOpen) CloseUnsafe();
        }

        public void OnPointerClick(PointerEventData eventData) => onClick?.Invoke();
    }
}