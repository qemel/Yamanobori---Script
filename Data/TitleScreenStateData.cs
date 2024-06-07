using System;
using R3;

namespace Rogue.Scripts.Data
{
    public sealed class TitleScreenStateData : IDisposable
    {
        public ReadOnlyReactiveProperty<ScreenTitleName> CurrentScreen => _currentScreen;
        private readonly ReactiveProperty<ScreenTitleName> _currentScreen = new(ScreenTitleName.None);

        private readonly CompositeDisposable _disposable = new();

        public void SetCurrentScreen(ScreenTitleName screenName)
        {
            if (_currentScreen.Value == screenName)
            {
                _currentScreen.Value = ScreenTitleName.None;
                return;
            }

            _currentScreen.Value = screenName;
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}