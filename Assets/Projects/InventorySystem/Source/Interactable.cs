using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Projects.InventorySystem.Source
{
    public class Interactable : MonoBehaviour, IPointerClickHandler
    {
        public event Action onLeftClick;
        public event Action onRightClick;

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left) onLeftClick?.Invoke();
            else if (eventData.button == PointerEventData.InputButton.Right) onRightClick?.Invoke();
        }
    }
}