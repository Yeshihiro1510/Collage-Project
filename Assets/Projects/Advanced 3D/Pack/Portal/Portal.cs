using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Projects.Advanced_3D
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] private Transform _endPoint;
        [SerializeField] private GameObject _message;
        [SerializeField] private GameObject _wallet;
        [SerializeField] private Button _button;

        private void Awake()
        {
            _button.OnTrigger.AddListener(obj => Process(obj).Forget());
        }

        private async UniTask Process(GameObject obj)
        {
            obj.transform.position = _endPoint.position;
            _message.SetActive(true);
            await UniTask.WaitUntil(() => Input.GetMouseButtonDown(0));
            _message.SetActive(false);
            _wallet.SetActive(true);
        }
    }
}