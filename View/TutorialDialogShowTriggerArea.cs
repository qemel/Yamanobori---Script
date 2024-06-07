using R3;
using Rogue.Scripts.View.Player;
using UnityEngine;

namespace Rogue.Scripts.View
{
    public sealed class TutorialDialogShowTriggerArea : MonoBehaviour
    {
        public Observable<Unit> OnEnterTrigger => _onEnterTrigger;
        private readonly Subject<Unit> _onEnterTrigger = new();

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovementView _))
            {
                _onEnterTrigger.OnNext(Unit.Default);
            }
        }

        private void OnDestroy()
        {
            _onEnterTrigger.OnCompleted();
        }
    }
}