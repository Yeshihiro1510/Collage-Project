using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Projects.UnitaskProj
{
    public abstract class Spell : MonoBehaviour
    {
        [SerializeField] protected Cube _cube;
        [SerializeField] protected Button _button;
        [SerializeField] protected float _countDown;
        [SerializeField] protected float _duration;
        protected bool _canCast = true;
        private Coroutine _coroutine;

        private void Awake()
        {
            _button.onClick.AddListener(DoSpell);
        }

        public void DoSpell()
        {
            if (_canCast) _coroutine = StartCoroutine(Routine());
        }

        public virtual void Stop()
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
                _coroutine = null;
            }

            _canCast = false;
            StartCoroutine(CountDown());
        }

        private IEnumerator Routine()
        {
            _canCast = false;
            yield return SpellRoutine();
            yield return CountDown();
        }

        private IEnumerator CountDown()
        {
            yield return new WaitForSeconds(_duration);
            _canCast = true;
        }

        protected abstract IEnumerator SpellRoutine();
    }
}