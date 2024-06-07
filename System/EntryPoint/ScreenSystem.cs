using System;
using R3;
using Rogue.Scripts.Data;
using VContainer.Unity;

namespace Rogue.Scripts.System.EntryPoint
{
    public sealed class ScreenSystem : IStartable, IDisposable
    {
        private readonly ScreenStateData _screenStateData;
        private readonly GameInputProvider _gameInputProvider;

        private readonly CompositeDisposable _disposable = new();

        public ScreenSystem(ScreenStateData screenStateData, GameInputProvider gameInputProvider)
        {
            _screenStateData = screenStateData;
            _gameInputProvider = gameInputProvider;
        }

        public void Start()
        {
            _gameInputProvider.Tab
                .Where(x => x)
                .Subscribe(_ => { _screenStateData.FlipIsSettingScreenActive(); })
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}