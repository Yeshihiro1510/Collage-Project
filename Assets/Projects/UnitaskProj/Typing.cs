using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Projects.UnitaskProj
{
    public class Typing : MonoBehaviour
    {
        [SerializeField] private Ease _messageEase;
        [SerializeField] private float _messageDuration;
        [SerializeField] private float _messageDelay;
        [SerializeField] private string[] _messages;

        [SerializeField] private Transform _content;
        [SerializeField] private TMP_Text _messagePref;

        private InputSystem_Actions _inputSystem;

        private void Awake()
        {
            _inputSystem = new InputSystem_Actions();
            _inputSystem.Player.Enable();
        }

        private void Start()
        {
            _ = Process();
        }

        private async UniTask Process()
        {
            var LMBCts = new CancellationTokenSource();
            _inputSystem.Player.LMB.performed += LMB;
            void LMB(InputAction.CallbackContext _) => LMBCts.Cancel();
            
            for (var i = 0; i < _messages.Length; i++)
            {
                var obj = Instantiate(_messagePref, _content);
                obj.maxVisibleCharacters = 0;
                obj.text = _messages[i];
                obj.alignment = i % 2 == 0 ? TextAlignmentOptions.Right : TextAlignmentOptions.Left;

                var tween = DOVirtual.Int(0, obj.text.Length, _messageDuration, v => obj.maxVisibleCharacters = v).SetEase(_messageEase);
                LMBCts.Token.Register(() => tween.Kill(true));
                _inputSystem.Player.RMB.performed += TogglePause;
                void TogglePause(InputAction.CallbackContext context) => tween.TogglePause();
                
                await tween.AsyncWaitForCompletion();
                if (i == 0) await LMBCts.Token.WaitUntilCanceled();
                await UniTask.WaitForSeconds(_messageDelay);

                ReNew(ref LMBCts);
                _inputSystem.Player.RMB.performed -= TogglePause;
            }

            _inputSystem.Player.LMB.performed -= LMB;
        }

        public static void ReNew(ref CancellationTokenSource cts)
        {
            if (cts != null)
            {
                cts.Cancel();
                cts.Dispose();
            }

            cts = new CancellationTokenSource();
        }
    }
}