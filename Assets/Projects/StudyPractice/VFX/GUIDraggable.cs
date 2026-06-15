using UnityEngine;
using UnityEngine.EventSystems;

namespace Projects.StudyPractice.VFX
{
    public class GUIDraggable : MonoBehaviour, IDragHandler, IBeginDragHandler
    {
        private Vector2 _offset;
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _offset = (Vector2)transform.position - eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            var pos = eventData.position;
            transform.position = new Vector3(pos.x + _offset.x, pos.y + _offset.y, transform.position.z);
        }
    }
}