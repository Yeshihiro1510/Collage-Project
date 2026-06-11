using UnityEngine;
using UnityEngine.EventSystems;

namespace Projects.StudyPractice.VFX
{
    public class InteractiveWindow : MonoBehaviour, IDragHandler
    {
        public void OnDrag(PointerEventData eventData)
        {
            var pos = eventData.position;
            transform.position = new Vector3(pos.x, pos.y, transform.position.z);
        }
    }
}