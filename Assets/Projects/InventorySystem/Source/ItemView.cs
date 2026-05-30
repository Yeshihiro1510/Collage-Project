using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Projects.InventorySystem.Source
{
    public class ItemView : MonoBehaviour
    {
        [field: SerializeField] public Image IconImage { get; private set; }
        [field: SerializeField] public TMP_Text AmountText { get; private set; }
    }
}