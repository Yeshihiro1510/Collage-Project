using Projects.StudyPractice.VFX;
using UnityEngine;
using UnityEngine.UI;

namespace Projects.StudyPractice.Gameplay
{
    public class PauseMenuView : RushAnimationPopup
    {
        [field: SerializeField] public Button ContinueButton { get; private set; }
        [field: SerializeField] public Button SettingsButton { get; private set; }
        [field: SerializeField] public Button BackButton { get; private set; }
        
        public void Open()
        {
            if (!_isOpen) OpenUnsafe();
        }

        public void Close()
        {
            if (_isOpen) CloseUnsafe();
        }
    }
}