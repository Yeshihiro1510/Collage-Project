using UnityEngine;

namespace Projects.Advanced_3D
{
    public class Coin : MonoBehaviour
    {
        [SerializeField, Min(0)] private float _value;
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.TryGetComponent(out CoinCollector collector))
            {
                collector.Add(_value);
                Destroy(gameObject);
            }
        }
    }
}