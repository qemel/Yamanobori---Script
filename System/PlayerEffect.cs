using Rogue.Scripts.View.Player;
using UnityEngine;

namespace Rogue.Scripts.System
{
    public sealed class PlayerEffect
    {
        private readonly PlayerEffectView _view;

        public PlayerEffect(PlayerEffectView view)
        {
            _view = view;
        }

        public void PlayDashEffect(Quaternion playerRotation)
        {
            _view.PlayDashEffect(playerRotation);
        }

        public void PlayJumpEffect()
        {
            _view.PlayJumpEffect();
        }

        public void PlayWallSlideEffect()
        {
            _view.PlayWallSlideEffect();
        }

        public void StopWallSlideEffect()
        {
            _view.StopWallSlideEffect();
        }

        public void PlayWallGrabEffect()
        {
            _view.PlayWallGrabEffect();
        }

        public void StopWallGrabEffect()
        {
            _view.StopWallGrabEffect();
        }

        public void PlayDeathEffect()
        {
            _view.PlayDeathEffect();
        }
        
        public void DestroyDeathEffect()
        {
            _view.DestroyDeathEffect();
        }
    }
}