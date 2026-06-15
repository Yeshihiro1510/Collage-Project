using Projects.InventorySystem.Source;
using Projects.InventorySystem.Source.New;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Projects.StudyPractice.Gameplay
{
    public class GameplayUIView : MonoBehaviour
    {
        [field: SerializeField] public Button PauseButton { get; private set; }
        [field: SerializeField] public InventoryView InventoryView { get; private set; }
        [field: SerializeField] public TMP_Text CurrencyText { get; private set; }
        [field: SerializeField] public Image HealthBar { get; private set; }
        [field: SerializeField] public GameObject Timer { get; private set; }
    }
}