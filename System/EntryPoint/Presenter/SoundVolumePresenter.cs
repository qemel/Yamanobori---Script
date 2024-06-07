using System;
using R3;
using Rogue.Scripts.Data;
using Rogue.Scripts.View.UI;
using VContainer.Unity;

namespace Rogue.Scripts.System.EntryPoint.Presenter
{
    public sealed class SoundVolumePresenter : IInitializable, IDisposable
    {
        private readonly SoundVolumeSlidersView _view;

        private readonly CompositeDisposable _disposable = new();

        public SoundVolumePresenter(SoundVolumeSlidersView view)
        {
            _view = view;
        }

        public void Initialize()
        {
            _view.SetBgmVolume(Sound.Setting.BgmVolume.CurrentValue);
            _view.SetSeVolume(Sound.Setting.SeVolume.CurrentValue);

            _view.BgmVolume.Subscribe(x => { Sound.Setting.SetBgmVolume(x); }).AddTo(_disposable);
            _view.SeVolume.Subscribe(x => { Sound.Setting.SetSeVolume(x); }).AddTo(_disposable);
            
            Sound.Setting.SetBgmVolume(Sound.Setting.BgmVolume.CurrentValue);
            Sound.Setting.SetSeVolume(Sound.Setting.SeVolume.CurrentValue);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}