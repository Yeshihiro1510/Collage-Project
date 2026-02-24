using System.Collections;
using UnityEngine;

namespace Projects.UnitaskProj
{
    public class WaterSpell : Spell
    {
        [SerializeField] private ParticleSystem _waterVFX;
        [SerializeField] private FireSpell _fireSpell;

        protected override IEnumerator SpellRoutine()
        {
            _waterVFX.Play();
            yield return new WaitForSeconds(_duration / 2);
            _fireSpell.Stop();
            yield return new WaitForSeconds(_duration / 2);
            _waterVFX.Stop();
        }
    }
}