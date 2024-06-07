using R3;
using Rogue.Scripts.View.Player;
using UnityEngine;

namespace Rogue.Scripts.View
{
    public sealed class StageClearAreaView : MonoBehaviour
    {
        public Observable<Unit> OnEnter => _onEnter;
        private readonly Subject<Unit> _onEnter = new();

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovementView _))
            {
                _onEnter.OnNext(Unit.Default);
            }
        }

        private void OnDestroy()
        {
            _onEnter.Dispose();
        }
    }
}