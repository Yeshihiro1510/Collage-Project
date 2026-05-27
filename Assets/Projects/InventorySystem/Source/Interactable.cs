using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Projects.InventorySystem.Source
{
    public class Interactable : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action<Interactable> onLeftClick;
        public event Action<Interactable> onRightClick;
        public event Action<Interactable> onEnter;
        public event Action<Interactable> onExit;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Left) onLeftClick?.Invoke(this);
            else if (eventData.button == PointerEventData.InputButton.Right) onRightClick?.Invoke(this);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            onEnter?.Invoke(this);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            onExit?.Invoke(this);
        }
    }
}