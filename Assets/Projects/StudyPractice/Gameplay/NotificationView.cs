using Projects.StudyPractice.VFX;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Projects.StudyPractice.Gameplay
{
    public class NotificationView : RushAnimationPopup
    {
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _bodyText;
        [SerializeField] private Image _iconImage;

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
    }
}