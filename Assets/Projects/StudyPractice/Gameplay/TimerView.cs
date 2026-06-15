using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Projects.StudyPractice.Gameplay
{
    public class TimerView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TMP_Text _text;

        public void Set(float time, float duration)
        {
            _image.fillAmount = 1 - time / duration;
            _text.text = $"{duration - time:00.00}";
        }
    }
}