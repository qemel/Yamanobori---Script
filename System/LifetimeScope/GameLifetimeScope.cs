using MessagePipe;
using Rogue.Scripts.Data;
using Rogue.Scripts.Data.Repository;
using Rogue.Scripts.System.EntryPoint;
using Rogue.Scripts.System.EntryPoint.Presenter;
using Rogue.Scripts.View;
using Rogue.Scripts.View.Debugger;
using Rogue.Scripts.View.Item;
using Rogue.Scripts.View.Player;
using Rogue.Scripts.View.UI;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer;
using VContainer.Unity;

namespace Rogue.Scripts.System.LifetimeScope
{
    public sealed class GameLifetimeScope : VContainer.Unity.LifetimeScope
    {
        [SerializeField] private bool _enableLogging;
        [SerializeField] private PlayerMovementView _playerMovementView;
        [SerializeField] private PlayerAnimationView _playerAnimationView;
        [SerializeField] private PlayerMovementInfo _playerMovementInfo;
        [SerializeField] private PlayerCameraView _playerCameraView;
        [SerializeField] private CameraControlInfo _cameraControlInfo;
        [SerializeField] private PlayerCameraFollowPointView _playerCameraFollowPointView;
        [SerializeField] private PlayerEffectView _playerEffectView;
        [SerializeField] private PlayerInput _gameInput;
        [SerializeField] private TransitionScreenView _transitionScreenView;
        [SerializeField] private PlayerDeathAreaView _playerDeathAreaView;
        [SerializeField] private DeathScreenView _deathScreenView;
        [SerializeField] private PlayerAudioView _playerAudioView;
        [SerializeField] private SoundVolumeSlidersView _soundVolumeSlidersView;
        [SerializeField] private SettingScreenView _settingScreenView;
        [SerializeField] private BackToGameButtonView _backToGameButtonView;
        [SerializeField] private BGMData _bgmData;
        [SerializeField] private CheckPointSE _checkPointSe;
        [SerializeField] private RetryButtonView _retryButtonView;
        [SerializeField] private StageClearAreaView _stageClearAreaView;
        [SerializeField] private StageClearScreenView _stageClearScreenView;
        [SerializeField] private InteractiveMusicData _interactiveMusicData;
        [SerializeField] private GameTimeView _gameTimeView;
        [SerializeField] private ControllerSensitivitySliderView _controllerSensitivitySliderView;
        [SerializeField] private ItemDashResetParent _itemDashResetParent;
        [SerializeField] private CheckpointParent _checkpointParent;
        [SerializeField] private FOVSliderView _fovSliderView;
        [SerializeField] private PlayerDebugMenu _playerDebugMenu;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterMessagePipe();

            builder.UseEntryPoints(entry =>
            {
                // Systems
                entry.Add<GameLoopSystem>();
                entry.Add<GameInputProvider>().AsSelf();
                entry.Add<CheckPointSystem>();
                entry.Add<ScreenSystem>();
                entry.Add<InteractiveMusicPlaySystem>();
                entry.Add<DashResetSystem>();

                // Controls
                // entry.Add<PlayerCameraControlSystemPad>().As<IPlayerCameraControlSystem>();
                entry.Add<PlayerCameraControlSystem>();
                entry.Add<PlayerControlSystem>();

                // Presenters
                entry.Add<SoundVolumePresenter>();
                entry.Add<ScreenUIPresenter>();
                entry.Add<SettingPresenter>();
                entry.Add<GameTimePresenter>();

                // Loggers (if needed)
                if (_enableLogging)
                {
                    entry.Add<PlayerMovementDataLoggingSystem>();
                }
            });

            builder.Register<PlayerMovementData>(Lifetime.Singleton);
            builder.Register<CameraControlData>(Lifetime.Singleton);
            builder.Register<TransitionScreen>(Lifetime.Singleton);
            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.Register<PlayerEntityData>(Lifetime.Singleton);
            builder.Register<PlayerMovement>(Lifetime.Singleton);
            builder.Register<PlayerEffect>(Lifetime.Singleton);
            builder.Register<CurrentCheckpointRepository>(Lifetime.Singleton);
            builder.Register<PlayerCameraMovement>(Lifetime.Singleton);
            builder.Register<ScreenStateData>(Lifetime.Singleton);
            builder.Register<GameTime>(Lifetime.Singleton);

            builder.RegisterComponent(_playerMovementView);
            builder.RegisterComponent(_playerMovementInfo);
            builder.RegisterComponent(_playerAnimationView);
            builder.RegisterComponent(_playerCameraView);
            builder.RegisterComponent(_cameraControlInfo);
            builder.RegisterComponent(_playerCameraFollowPointView);
            builder.RegisterComponent(_playerEffectView);
            builder.RegisterComponent(_gameInput);
            builder.RegisterComponent(_transitionScreenView);
            builder.RegisterComponent(_playerDeathAreaView);
            builder.RegisterComponent(_deathScreenView);
            builder.RegisterComponent(_checkpointParent);
            builder.RegisterComponent(_playerAudioView);
            builder.RegisterComponent(_soundVolumeSlidersView);
            builder.RegisterComponent(_settingScreenView);
            builder.RegisterComponent(_backToGameButtonView);
            builder.RegisterComponent(_bgmData);
            builder.RegisterComponent(_checkPointSe);
            builder.RegisterComponent(_retryButtonView);
            builder.RegisterComponent(_stageClearAreaView);
            builder.RegisterComponent(_stageClearScreenView);
            builder.RegisterComponent(_interactiveMusicData);
            builder.RegisterComponent(_gameTimeView);
            builder.RegisterComponent(_controllerSensitivitySliderView);
            builder.RegisterComponent(_itemDashResetParent);
            builder.RegisterComponent(_fovSliderView);
            
            // Debug
            builder.RegisterComponent(_playerDebugMenu);
        }
    }
}