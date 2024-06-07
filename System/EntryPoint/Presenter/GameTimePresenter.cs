using System;
using R3;
using Rogue.Scripts.Data;
using Rogue.Scripts.View.UI;
using VContainer.Unity;

namespace Rogue.Scripts.System.EntryPoint.Presenter
{
    public sealed class GameTimePresenter : IInitializable, IDisposable
    {
        private readonly GameTime _model;
        private readonly GameTimeView _view;

        private readonly CompositeDisposable _disposable = new();

        public GameTimePresenter(GameTime model, GameTimeView view)
        {
            _model = model;
            _view = view;
        }

        public void Initialize()
        {
            _model.ValueSec.Subscribe(_view.SetCurrent).AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}