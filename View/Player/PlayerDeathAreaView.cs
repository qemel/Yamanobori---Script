using R3;
using UnityEngine;

namespace Rogue.Scripts.View.Player
{
    public sealed class PlayerDeathAreaView : MonoBehaviour
    {
        public Observable<Unit> OnPlayerTouched => _onPlayerTouched;
        private readonly Subject<Unit> _onPlayerTouched = new();

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovementView _))
            {
                _onPlayerTouched.OnNext(Unit.Default);
            }
        }

        private void OnDestroy()
        {
            _onPlayerTouched.Dispose();
        }
    }
}