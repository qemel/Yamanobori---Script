using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using MessagePipe;
using R3;
using Rogue.Scripts.Data;
using Rogue.Scripts.Data.Message;
using Rogue.Scripts.Data.Repository;
using Rogue.Scripts.View;
using Rogue.Scripts.View.Player;
using Rogue.Scripts.View.UI;
using UnityEngine;
using VContainer.Unity;

namespace Rogue.Scripts.System.EntryPoint
{
    public sealed class GameLoopSystem : IStartable, ITickable, IDisposable
    {
        private readonly CurrentCheckpointRepository _currentCheckpointRepository;
        private readonly ScreenStateData _screenStateData;
        private readonly PlayerEntityData _playerEntityData;
        private readonly PlayerMovementData _playerMovementData;
        private readonly CameraControlData _cameraControlData;
        private readonly GameTime _gameTime;

        private readonly SceneLoader _sceneLoader;
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerEffect _playerEffect;

        private readonly DeathScreenView _deathScreenView;
        private readonly PlayerDeathAreaView _playerDeathAreaView;
        private readonly PlayerAnimationView _playerAnimationView;
        private readonly PlayerAudioView _playerAudioView;
        private readonly RetryButtonView _retryButtonView;
        private readonly StageClearAreaView _stageClearAreaView;
        private readonly GameTimeView _gameTimeView;

        private readonly ISubscriber<PlayerKillableHitMessage> _playerKillableHitSubscriber;
        private readonly ISubscriber<RestartMessage> _restartMessageSubscriber;

        private readonly CancellationTokenSource _cancellationTokenSource = new();
        private CancellationToken _cancellationToken;
        private readonly CompositeDisposable _disposable = new();

        public GameLoopSystem(PlayerAnimationView playerAnimationView, PlayerDeathAreaView playerDeathAreaView,
            PlayerEntityData playerEntityData, PlayerMovement playerMovement, PlayerEffect playerEffect,
            DeathScreenView deathScreenView, CurrentCheckpointRepository currentCheckpointRepository,
            ISubscriber<PlayerKillableHitMessage> playerKillableHitSubscriber, PlayerAudioView playerAudioView,
            RetryButtonView retryButtonView, ScreenStateData screenStateData,
            StageClearAreaView stageClearAreaView, GameTime gameTime,
            PlayerMovementData playerMovementData, SceneLoader sceneLoader, CameraControlData cameraControlData,
            ISubscriber<RestartMessage> restartMessageSubscriber, GameTimeView gameTimeView)
        {
            _playerAnimationView = playerAnimationView;
            _playerDeathAreaView = playerDeathAreaView;
            _playerEntityData = playerEntityData;
            _playerMovement = playerMovement;
            _playerEffect = playerEffect;
            _deathScreenView = deathScreenView;
            _currentCheckpointRepository = currentCheckpointRepository;
            _playerKillableHitSubscriber = playerKillableHitSubscriber;
            _playerAudioView = playerAudioView;
            _retryButtonView = retryButtonView;
            _screenStateData = screenStateData;
            _stageClearAreaView = stageClearAreaView;
            _gameTime = gameTime;
            _playerMovementData = playerMovementData;
            _sceneLoader = sceneLoader;
            _cameraControlData = cameraControlData;
            _restartMessageSubscriber = restartMessageSubscriber;
            _gameTimeView = gameTimeView;
        }

        public void Start()
        {
            _cancellationToken = _cancellationTokenSource.Token;

            _playerDeathAreaView.OnPlayerTouched.Subscribe(_ => { Retry(); }).AddTo(_disposable);
            _playerKillableHitSubscriber.Subscribe(_ => { Retry(); }).AddTo(_disposable);

            _retryButtonView.OnPressed.Subscribe(_ =>
            {
                _screenStateData.FlipIsSettingScreenActive();
                Retry();
            }).AddTo(_disposable);

            _stageClearAreaView.OnEnter.Subscribe(_ =>
            {
                _screenStateData.FlipIsClearScreenActive();
                _playerMovementData.CanMove = false;
                _cameraControlData.CanMove = false;
                _cameraControlData.IsGameClear = true;
                _gameTimeView.Deactivate();
            }).AddTo(_disposable);

            _restartMessageSubscriber
                .Subscribe(_ =>
                {
                    Time.timeScale = 1;
                    _sceneLoader.LoadAsync("Game", _cancellationToken).Forget();
                })
                .AddTo(_disposable);
        }

        public void Tick()
        {
            if (!_playerEntityData.IsMoved) return;

            _gameTime.UpdateTime(Time.deltaTime);
        }

        private void Retry()
        {
            KillPlayer();
            RestartFromCheckPointAsync().Forget();
        }

        private void KillPlayer()
        {
            _playerEntityData.Kill();
            _playerMovement.Stop();
            _playerEffect.PlayDeathEffect();
            _playerAnimationView.Kill();
        }

        private async UniTask RestartFromCheckPointAsync()
        {
            _deathScreenView.PlayDeathScreen().Forget();
            _playerAudioView.PlayDeathSound();
            await UniTask.Delay(TimeSpan.FromSeconds(0.6f), cancellationToken: _cancellationToken);
            _playerEffect.DestroyDeathEffect();
            _playerAudioView.PlayReviveSound();
            _playerEntityData.Revive();
            _playerMovement.Resume();
            var checkpoint = _currentCheckpointRepository.Load();
            _playerMovement.SetPosition(checkpoint.Position + new Vector3(0, 1, 0));
            _playerMovement.SetFacingDirection(checkpoint.PlayerDirection);
            _playerMovement.SetUseGravity(true);
            _cameraControlData.PlanarDirection = checkpoint.PlayerDirection;
            _playerAnimationView.Revive();

            if (SettingRepository.IsHardcoreMode)
            {
                _gameTime.Reset();
                _playerEntityData.IsMoved = false;
            }
        }

        public void Dispose()
        {
            _cancellationTokenSource.Cancel();
            _disposable?.Dispose();
        }
    }
}