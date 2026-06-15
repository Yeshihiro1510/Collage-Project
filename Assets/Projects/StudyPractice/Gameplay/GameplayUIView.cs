using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Projects.StudyPractice.Gameplay
{
    public class GameplayUIView : MonoBehaviour
    {
        [field: SerializeField] public Transform Content { get; private set; }
        [field: SerializeField] public Button PauseButton { get; private set; }
        [field: SerializeField] public MoneyMenuView MoneyMenu { get; private set; }
        [field: SerializeField] public TimerView Timer { get; private set; }
        [field: SerializeField] public Button InventoryButton { get; private set; }
        [field: SerializeField] public Button ShopButton { get; private set; }
    }
}