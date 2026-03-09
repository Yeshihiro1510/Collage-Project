using UnityEngine;

namespace Projects.Advanced_3D
{
    public class Damagable : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Movement movement))
                movement.Kill();
        }
    }
}