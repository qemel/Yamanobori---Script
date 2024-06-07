using UnityEngine;

namespace Rogue.Scripts.View.Player
{
    public sealed class PlayerEffectView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _dashEffect;
        [SerializeField] private ParticleSystem _jumpEffect;
        [SerializeField] private ParticleSystem _wallSlideEffect;
        [SerializeField] private ParticleSystem _wallGrabEffect;
        [SerializeField] private ParticleSystem _playerDeathEffect;

        private ParticleSystem _playerDeathEffectInstance;

        public void PlayDashEffect(Quaternion playerRotation)
        {
            var rotation = Quaternion.AngleAxis(180, Vector3.up);
            Instantiate(_dashEffect, transform.position, playerRotation * rotation).Play();
        }

        public void PlayJumpEffect()
        {
            var randomRotation = Quaternion.AngleAxis(Random.Range(0, 360), Vector3.up);
            Instantiate(_jumpEffect, transform.position, randomRotation).Play();
        }

        public void PlayWallSlideEffect()
        {
            _wallSlideEffect.Play();
        }

        public void StopWallSlideEffect()
        {
            _wallSlideEffect.Stop();
        }

        public void PlayWallGrabEffect()
        {
            // _wallGrabEffect.Play();
        }

        public void StopWallGrabEffect()
        {
            // _wallGrabEffect.Stop();
        }

        public void PlayDeathEffect()
        {
            _playerDeathEffectInstance = Instantiate(_playerDeathEffect, transform.position, Quaternion.identity);
            _playerDeathEffectInstance.Play();
        }

        public void DestroyDeathEffect()
        {
            if (_playerDeathEffectInstance == null)
            {
                Debug.LogWarning("Player death effect instance is null");
                return;
            }

            Destroy(_playerDeathEffectInstance.gameObject);
        }
    }
}