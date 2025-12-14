using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMenuView : MonoBehaviour
{
    public int maxLevelsOnPage;
    public int unlockedLevelsCount;
        
    public LevelView levelViewPref;
    public RectTransform pagePref;
    
    public GridLayoutGroup pageParent;

    public LevelData[] levelData;
    
    private readonly List<RectTransform> _pages = new();
    private readonly List<LevelView> _levelViews = new();
    
    private void Start()
    {
        _pages.Add(Instantiate(pagePref, pageParent.transform));

        for (var i = 0; i < levelData.Length; i++)
        {
            var currentPageIndex = i / maxLevelsOnPage;
            if (_pages.Count - 1 < currentPageIndex) _pages.Add(Instantiate(pagePref, pageParent.transform));
            
            var newLevelView = Instantiate(levelViewPref, _pages[currentPageIndex]);
            newLevelView.Init(i + 1, levelData[i].starsCount, i > unlockedLevelsCount);
            _levelViews.Add(newLevelView);
        }
        
        pageParent.constraintCount = _pages.Count;
    }

    [Serializable]
    public struct LevelData
    {
        public int starsCount;
    }
}