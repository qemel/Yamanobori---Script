using System;
using R3;
using Rogue.Scripts.Data;
using Rogue.Scripts.View.UI;
using UnityEngine;
using unityroom.Api;
using VContainer.Unity;

namespace Rogue.Scripts.System.EntryPoint.Presenter
{
    public sealed class ScreenUIPresenter : IInitializable, IDisposable
    {
        private readonly ScreenStateData _screenStateData;
        private readonly SettingScreenView _settingScreenView;
        private readonly StageClearScreenView _stageClearScreenView;
        private readonly BackToGameButtonView _backToGameButtonView;

        private readonly GameTime _gameTime;

        private readonly CompositeDisposable _disposable = new();

        public ScreenUIPresenter(ScreenStateData screenStateData, SettingScreenView settingScreenView,
            BackToGameButtonView backToGameButtonView, StageClearScreenView stageClearScreenView, GameTime gameTime)
        {
            _screenStateData = screenStateData;
            _settingScreenView = settingScreenView;
            _backToGameButtonView = backToGameButtonView;
            _stageClearScreenView = stageClearScreenView;
            _gameTime = gameTime;
        }

        public void Initialize()
        {
            _screenStateData.IsSettingScreenActive.Subscribe(isActive =>
            {
                if (isActive)
                {
                    _settingScreenView.Activate();
                    Cursor.lockState = CursorLockMode.None;
                    Time.timeScale = 0;
                }
                else
                {
                    _settingScreenView.Deactivate();
                    Cursor.lockState = CursorLockMode.Locked;
                    Time.timeScale = 1;
                }
            }).AddTo(_disposable);

            _screenStateData.IsClearScreenActive.Subscribe(isActive =>
            {
                if (isActive)
                {
                    UnityroomApiClient.Instance.SendScore(1, _gameTime.ValueSec.CurrentValue,
                        ScoreboardWriteMode.HighScoreAsc);
                    _stageClearScreenView.Init(_gameTime);
                    _stageClearScreenView.Activate();
                    Cursor.lockState = CursorLockMode.None;
                }
                else
                {
                    _stageClearScreenView.Deactivate();
                    Cursor.lockState = CursorLockMode.Locked;
                }
            }).AddTo(_disposable);

            _backToGameButtonView.OnPressed.Subscribe(_ => { _screenStateData.FlipIsSettingScreenActive(); })
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}