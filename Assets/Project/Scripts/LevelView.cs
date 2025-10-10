using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelView : MonoBehaviour
{
    [SerializeField] private TMP_Text _levelNumberText;
    [SerializeField] private Button _button;
    [SerializeField] private Image[] _stars;
    [SerializeField] private Image _lock;

    public void Init(int levelNumber, int starsCount, bool locked)
    {
        if (starsCount is < 0 or > 3) throw new Exception($"Invalid number of stars: {starsCount}.");
        
        _levelNumberText.text = levelNumber.ToString();
        var starSprite = "GUI".GetSpriteFromTexture("GUI_24");
        for (var i = 0; i < starsCount; i++)
            _stars[i].sprite = starSprite;
        _lock.gameObject.SetActive(locked);
        _button.enabled = locked;
    }
}