using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using Rogue.Scripts.Data.Repository;
using Rogue.Scripts.View.UI;
using VContainer.Unity;

namespace Rogue.Scripts.System.EntryPoint.Presenter
{
    public sealed class TitleUIPresenter : IInitializable, IDisposable
    {
        private readonly SceneLoader _sceneLoader;
        private readonly GameStartButtonView _startButtonView;
        private readonly GameHardcoreStartButtonView _hardcoreStartButtonView;

        private readonly CancellationTokenSource _cancellationTokenSource = new();
        private CancellationToken CancellationToken => _cancellationTokenSource.Token;
        private readonly CompositeDisposable _disposable = new(1);

        public TitleUIPresenter(SceneLoader sceneLoader, GameStartButtonView startButtonView,
            GameHardcoreStartButtonView hardcoreStartButtonView)
        {
            _sceneLoader = sceneLoader;
            _startButtonView = startButtonView;
            _hardcoreStartButtonView = hardcoreStartButtonView;
        }

        public void Initialize()
        {
            _startButtonView.OnPressed
                .Subscribe(_ =>
                {
                    SettingRepository.IsHardcoreMode = false;
                    _sceneLoader.LoadAsync("Game", CancellationToken).Forget();
                })
                .AddTo(_disposable);

            _hardcoreStartButtonView.OnPressed
                .Subscribe(_ =>
                {
                    SettingRepository.IsHardcoreMode = true;
                    _sceneLoader.LoadAsync("Game", CancellationToken).Forget();
                })
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _cancellationTokenSource?.Cancel();
            _disposable?.Dispose();
        }
    }
}