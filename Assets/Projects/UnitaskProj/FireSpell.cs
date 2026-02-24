using System.Collections;
using UnityEngine;

namespace Projects.UnitaskProj
{
    public class FireSpell : Spell
    {
        [SerializeField] private ParticleSystem _fireVFX;

        protected override IEnumerator SpellRoutine()
        {
            _fireVFX.Play();
            while (_cube.Progress < 1f)
            {
                _cube.Progress += Time.deltaTime/_duration;
                _cube.Progress = Mathf.Clamp(_cube.Progress, 0f, 1f);
                yield return null;
            }
            _fireVFX.Stop();
        }

        public override void Stop()
        {
            base.Stop();
            _fireVFX.Stop();
        }
    }
}