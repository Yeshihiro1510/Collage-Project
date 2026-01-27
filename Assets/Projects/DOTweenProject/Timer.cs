using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Projects.DOTweenProject
{
    public class Timer : MonoBehaviour
    {
        public float Time;
        [SerializeField] private Image _progressBar;

        private void Start()
        {
            DOVirtual.Float(1, 0, Time, v => _progressBar.fillAmount = v);
        }
    }
}