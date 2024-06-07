using AnnulusGames.LucidTools.Audio;
using MessagePipe;
using Rogue.Scripts.Data.Message;
using Rogue.Scripts.View.Player;
using UnityEngine;
using VContainer;

namespace Rogue.Scripts.View.Item
{
    public sealed class SpringView : MonoBehaviour
    {
        [SerializeField] private float _jumpForce = 20f;
        [SerializeField] private AudioClip _springSound;
        [SerializeField] private ParticleSystem _springEffect;
        
        private IPublisher<SpringHitMessage> _publisher;

        [Inject]
        public void Construct(IPublisher<SpringHitMessage> publisher)
        {
            _publisher = publisher;
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.TryGetComponent(out PlayerMovementView _))
            {
                _publisher.Publish(new SpringHitMessage(transform.up, _jumpForce));
                LucidAudio.PlaySE(_springSound).SetTimeSamples();
                _springEffect.Play();
            }
        }
    }
}