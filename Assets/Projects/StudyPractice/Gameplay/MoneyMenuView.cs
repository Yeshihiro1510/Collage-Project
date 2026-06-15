using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Projects.StudyPractice.Gameplay
{
    public class MoneyMenuView : MonoBehaviour
    {
        [field: SerializeField] public TMP_Text Text { get; set; }
        [field: SerializeField] public Button AddMoneyButton { get; set; }
    }
}