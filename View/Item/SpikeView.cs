using MessagePipe;
using Rogue.Scripts.Data.Message;
using Rogue.Scripts.View.Player;
using UnityEngine;
using VContainer;

namespace Rogue.Scripts.View.Item
{
    public sealed class SpikeView : MonoBehaviour
    {
        private IPublisher<PlayerKillableHitMessage> _publisher;

        [Inject]
        public void Construct(IPublisher<PlayerKillableHitMessage> publisher)
        {
            _publisher = publisher;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovementView _))
            {
                _publisher.Publish(new PlayerKillableHitMessage());
            }
        }
    }
}