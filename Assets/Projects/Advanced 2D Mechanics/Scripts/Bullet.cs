using Cysharp.Threading.Tasks;
using UnityEngine;
using Yeshi_Pool;

namespace Projects.Advanced_2D_Mechanics
{
    public class Bullet : MonoPoolable
    {
        public async UniTask Launch(Vector2 direction, float speed, float amplitude)
        {
            Vector2 start = transform.position;
            Vector2 end = start + direction;

            for (float progress = 0; progress < 1; progress += Time.deltaTime * speed)
            {
                if (destroyCancellationToken.IsCancellationRequested) return;
                var lerp = Vector2.Lerp(start, end, progress);
                lerp.y += Mathf.Sin(progress * Mathf.PI) * amplitude;
                
                transform.position = lerp;

                await UniTask.Yield(cancellationToken: destroyCancellationToken);
            }

            foreach (var c in Physics2D.OverlapCircleAll(transform.position, 1f))
            {
                if (destroyCancellationToken.IsCancellationRequested) return;
                if (c.transform.TryGetComponent(out Killable killable)) killable.Damage();
            }

            Release();
        }
    }
}