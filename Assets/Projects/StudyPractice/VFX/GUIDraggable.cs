using UnityEngine;
using UnityEngine.EventSystems;

namespace Projects.StudyPractice.VFX
{
    public class GUIDraggable : MonoBehaviour, IDragHandler
    {
        public void OnDrag(PointerEventData eventData)
        {
            var pos = eventData.position;
            // var offset = (Vector2)transform.position - pos;
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
    }
}