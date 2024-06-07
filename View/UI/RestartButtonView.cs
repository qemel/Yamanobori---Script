using MessagePipe;
using R3;
using Rogue.Scripts.Data.Message;
using VContainer;

namespace Rogue.Scripts.View.UI
{
    public sealed class RestartButtonView : ButtonBaseView
    {
        private IPublisher<RestartMessage> _publisher;

        [Inject]
        public void Construct(IPublisher<RestartMessage> publisher)
        {
            _publisher = publisher;
        }

        protected override void PostStart()
        {
            OnPressed.Subscribe(_ => _publisher.Publish(new RestartMessage())).AddTo(this);
        }
    }
}