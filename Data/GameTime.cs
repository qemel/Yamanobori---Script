using System;
using R3;

namespace Rogue.Scripts.Data
{
    public sealed class GameTime : IDisposable
    {
        public ReadOnlyReactiveProperty<float> ValueSec => _valueSec;
        private readonly ReactiveProperty<float> _valueSec = new(0);

        private readonly CompositeDisposable _disposable = new();

        public GameTime()
        {
            _valueSec.AddTo(_disposable);
        }

        public void UpdateTime(float deltaTime)
        {
            _valueSec.Value += deltaTime;
        }

        public void Reset()
        {
            _valueSec.Value = 0;
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}