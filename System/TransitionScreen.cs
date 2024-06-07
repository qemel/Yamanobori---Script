using System.Threading;
using Cysharp.Threading.Tasks;
using Rogue.Scripts.View.UI;

namespace Rogue.Scripts.System
{
    public sealed class TransitionScreen
    {
        private readonly TransitionScreenView _view;

        public TransitionScreen(TransitionScreenView view)
        {
            _view = view;
        }

        public async UniTask ShowAsync(CancellationToken cancellationToken)
        {
            await _view.ShowAsync(cancellationToken);
        }

        public async UniTask HideAsync(CancellationToken cancellationToken)
        {
            await _view.HideAsync(cancellationToken);
        }
    }
}