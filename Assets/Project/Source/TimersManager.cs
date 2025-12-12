using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TimersManager : MonoBehaviour
{
    [SerializeField] private Timer _firstTimer;
    [SerializeField] private Timer _secondTimer;

    [SerializeField] private TMP_Text _firstTimerText;
    [SerializeField] private TMP_Text _resourcesText;

    [SerializeField] private Button _timerActivationButton;

    [SerializeField] private Image _firstTimerImage;
    [SerializeField] private Image _secondTimerImage;

    private int FirstTimerRunCount
    {
        get => _firstTimerRunCount;
        set
        {
            _firstTimerRunCount = value;
            _firstTimerText.text = $"Run count: {_firstTimerRunCount}";
        }
    }

    private int _firstTimerRunCount;

    private int ResourcesCount
    {
        get => _resourcesCount;
        set
        {
            _resourcesCount = value;
            _resourcesText.text = $"Resources: {_resourcesCount}";
        }
    }

    private int _resourcesCount;

    private void Start()
    {
        FirstTimerRunCount = 0;
        ResourcesCount = 0;
        
        // First timer way
        _timerActivationButton.onClick.AddListener(() =>
        {
            if (ResourcesCount < 5) return;
            ResourcesCount -= 5;
            _firstTimer.Run();
        });
        _firstTimer.OnTimerEnd.AddListener(() =>
        {
            FirstTimerRunCount++;
            _firstTimerImage.fillAmount = 1;
        });
        _firstTimer.OnTick.AddListener(callback =>
            _firstTimerImage.fillAmount = (callback.Item2 - callback.Item1).Normalize(0, callback.Item2));

        // Second timer way
        _secondTimer.OnTimerEnd.AddListener(() =>
        {
            _secondTimer.Run();
            ResourcesCount += 3;
        });
        _secondTimer.OnTick.AddListener(callback =>
            _secondTimerImage.fillAmount = callback.Item1.Normalize(0, callback.Item2));

        _secondTimer.Run();
    }
}