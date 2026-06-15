using Projects.StudyPractice.VFX;
using UnityEngine;
using UnityEngine.UI;

namespace Projects.StudyPractice
{
    public class SettingsView : RotationAnimationPopup
    {
        [field: SerializeField] public Button CloseButton { get; private set; }
        [field: SerializeField] public Slider GeneralSlider { get; private set; }
        [field: SerializeField] public Slider MusicSlider { get; private set; }
        [field: SerializeField] public Slider SFXSlider { get; private set; }
        [field: SerializeField] public Toggle NotificationsToggle { get; private set; }
        [field: SerializeField] public Button ResetButton { get; private set; }
    }
}