using System;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Projects.Advanced_2D_Mechanics
{
    public class Archer : MonoBehaviour
    {
        [SerializeField] private Shooter _shooter;
            
        [SerializeField] private Vector2 _direction;
        [SerializeField] private float _delay;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, (Vector2)transform.position + _direction);
        }

        private async UniTask Start()
        {
            while (!destroyCancellationToken.IsCancellationRequested)
            {
                _shooter.Shoot(_direction);
                await UniTask.WaitForSeconds(_delay, cancellationToken: destroyCancellationToken);
            }
        }
    }
}