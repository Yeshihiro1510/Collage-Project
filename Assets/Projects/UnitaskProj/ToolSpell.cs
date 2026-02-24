using System.Collections;
using DG.Tweening;
using UnityEngine;

namespace Projects.UnitaskProj
{
    public class ToolSpell : Spell
    {
        [SerializeField] private Transform _tool;
        [SerializeField] private Transform _startPoint;
        [SerializeField] private Transform _endPoint;

        protected override IEnumerator SpellRoutine()
        {
            yield return _tool.DOMove(_endPoint.position, _duration / 4).SetEase(Ease.OutBack).WaitForCompletion();
            var progress = _cube.Progress / 3f;
            for (int i = 0; i < 3; i++)
            {
                yield return _tool.DOPunchRotation(Vector3.right * 60, _duration / 6, 6).OnComplete(() =>
                {
                    _cube.Progress -= progress;
                }).WaitForCompletion();
            }

            yield return _tool.DOMove(_startPoint.position, _duration / 4).SetEase(Ease.OutBack).WaitForCompletion();
        }
    }
}