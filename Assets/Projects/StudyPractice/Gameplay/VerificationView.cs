using Projects.StudyPractice.VFX;
using UnityEngine;
using UnityEngine.UI;

namespace Projects.StudyPractice.Gameplay
{
    public class VerificationView : RotationAnimationPopup
    {
        [field: SerializeField] public Button Yes { get; private set; }
        [field: SerializeField] public Button No { get; private set; }
    }
}