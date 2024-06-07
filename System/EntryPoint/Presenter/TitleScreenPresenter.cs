using System;
using R3;
using Rogue.Scripts.Data;
using Rogue.Scripts.View.UI;
using VContainer.Unity;

namespace Rogue.Scripts.System.EntryPoint.Presenter
{
    public sealed class TitleScreenPresenter : IInitializable, IDisposable
    {
        private readonly TitleScreenStateData _data;
        private readonly TitleScreenParent _views;

        private readonly CompositeDisposable _disposable = new();

        public TitleScreenPresenter(TitleScreenStateData data, TitleScreenParent views)
        {
            _data = data;
            _views = views;
        }

        public void Initialize()
        {
            _data.CurrentScreen
                .Subscribe(screen =>
                {
                    foreach (var component in _views.TitleScreenComponents)
                    {
                        component.SetActive(false);
                    }

                    if (screen == ScreenTitleName.None) return;
                    
                    _views.Get(screen).SetActive(true);
                }).AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}