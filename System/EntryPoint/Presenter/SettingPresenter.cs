using System;
using R3;
using Rogue.Scripts.Data.Repository;
using Rogue.Scripts.View.UI;
using UnityEngine;
using VContainer.Unity;

namespace Rogue.Scripts.System.EntryPoint.Presenter
{
    public sealed class SettingPresenter : IInitializable, IDisposable
    {
        private readonly ControllerSensitivitySliderView _senView;
        private readonly FOVSliderView _fovView;

        private readonly CompositeDisposable _disposable = new();

        public SettingPresenter(ControllerSensitivitySliderView senView, FOVSliderView fovView)
        {
            _senView = senView;
            _fovView = fovView;
        }

        public void Initialize()
        {
            _senView.SetSlider(SettingRepository.ControllerSensitivityBase);
            _senView.SetText(SettingRepository.ControllerSensitivityBase);
            _fovView.SetSlider(SettingRepository.FovBase);
            _fovView.SetText((int)SettingRepository.Fov);

            _senView.Sensitivity
                .DistinctUntilChanged()
                .Subscribe(val =>
                {
                    SettingRepository.ControllerSensitivityBase = val;
                    _senView.SetText(val);
                })
                .AddTo(_disposable);

            _fovView.Fov
                .DistinctUntilChanged()
                .Subscribe(val =>
                {
                    SettingRepository.FovBase = val;
                    if (Camera.main != null)
                    {
                        Camera.main.fieldOfView = SettingRepository.Fov;
                    }

                    _fovView.SetText((int)SettingRepository.Fov);
                })
                .AddTo(_disposable);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}