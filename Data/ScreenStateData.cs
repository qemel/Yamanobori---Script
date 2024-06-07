using System;
using R3;

namespace Rogue.Scripts.Data
{
    public sealed class ScreenStateData : IDisposable
    {
        public ReadOnlyReactiveProperty<bool> IsSettingScreenActive => _isSettingScreenActive;
        private readonly ReactiveProperty<bool> _isSettingScreenActive = new(false);

        public ReadOnlyReactiveProperty<bool> IsClearScreenActive => _isClearScreenActive;
        private readonly ReactiveProperty<bool> _isClearScreenActive = new(false);

        private readonly CompositeDisposable _disposable = new();

        public ScreenStateData()
        {
            _isSettingScreenActive.AddTo(_disposable);
        }


        public void FlipIsSettingScreenActive()
        {
            // クリア画面中は設定画面を表示しない
            if (_isClearScreenActive.Value) return;
            
            _isSettingScreenActive.Value = !_isSettingScreenActive.Value;
        }

        public void FlipIsClearScreenActive()
        {
            _isClearScreenActive.Value = !_isClearScreenActive.Value;
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}