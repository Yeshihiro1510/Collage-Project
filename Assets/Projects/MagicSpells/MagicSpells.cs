using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MagicSpells : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _subject;
    [SerializeField] private Button _firstButton;
    [SerializeField] private Button _secondButton;
    [SerializeField] private Button _thirdButton;

    private bool _isFirstBusy;
    private bool _isSecondBusy;
    private bool _isThirdBusy;

    private void Awake()
    {
        _firstButton.onClick.AddListener(UseFirst);
        _secondButton.onClick.AddListener(UseSecond);
        _thirdButton.onClick.AddListener(UseThird);
    }

    private void UseFirst()
    {
        if (_isFirstBusy) return;
        _subject.color = Color.red;
        StartCoroutine(Recharge(1, 3f));
    }

    private void UseSecond()
    {
        if (_isSecondBusy) return;
        _subject.color = Color.blue;
        StartCoroutine(Recharge(2, 5f));
    }

    private void UseThird()
    {
        if (_isThirdBusy) return;
        _subject.transform.localScale = Vector3.one * 5f;
        StartCoroutine(Recharge(3, 15f));
    }

    private IEnumerator Recharge(int spellIndex, float duration)
    {
        if (spellIndex == 1) _isFirstBusy = true;
        if (spellIndex == 2) _isSecondBusy = true;
        if (spellIndex == 3) _isThirdBusy = true;
        yield return new WaitForSeconds(duration);
        if (spellIndex == 1) _isFirstBusy = false;
        if (spellIndex == 2) _isSecondBusy = false;
        if (spellIndex == 3) _isThirdBusy = false;
    }
}