using System;
using AnnulusGames.LucidTools.Audio;
using R3;
using Rogue.Scripts.Data;
using Rogue.Scripts.Data.Repository;
using VContainer.Unity;

namespace Rogue.Scripts.System.EntryPoint
{
    public sealed class InteractiveMusicPlaySystem : IStartable, ITickable, IDisposable
    {
        private readonly BGMData _bgmData;
        private readonly InteractiveMusicData _interactiveMusicData;
        private readonly PlayerMovement _playerMovement;
        private readonly PlayerEntityData _playerEntityData;

        private bool _isTriggeredP2;
        private bool _isTriggeredP3;
        private bool _isTriggeredP4;
        private const float FadeTime = 0.8f;

        private readonly CompositeDisposable _disposable = new();

        public InteractiveMusicPlaySystem(BGMData bgmData, InteractiveMusicData interactiveMusicData,
            PlayerMovement playerMovement, PlayerEntityData playerEntityData)
        {
            _bgmData = bgmData;
            _interactiveMusicData = interactiveMusicData;
            _playerMovement = playerMovement;
            _playerEntityData = playerEntityData;
        }

        public void Start()
        {
            if (LucidAudio.ActiveBGMCount == 0)
            {
                BGMRepository.SetAllBGM(
                    LucidAudio.PlayBGM(_bgmData.MainP1).SetLoop(),
                    LucidAudio.PlayBGM(_bgmData.MainP2).SetVolume(0).SetLoop(),
                    LucidAudio.PlayBGM(_bgmData.MainP3).SetVolume(0).SetLoop(),
                    LucidAudio.PlayBGM(_bgmData.MainP4).SetVolume(0).SetLoop()
                );
            }
            else
            {
                BGMRepository.MainP1.SetVolume(1f);
                BGMRepository.MainP2.FadeVolume(0f, 0.5f);
                BGMRepository.MainP3.FadeVolume(0f, 0.5f);
                BGMRepository.MainP4.FadeVolume(0f, 0.5f);
            }

            if (SettingRepository.IsHardcoreMode)
            {
                _playerEntityData.OnRevive.Subscribe(x =>
                {
                    _isTriggeredP2 = false;
                    _isTriggeredP3 = false;
                    _isTriggeredP4 = false;

                    BGMRepository.MainP2.FadeVolume(0f, 0.5f);
                    BGMRepository.MainP3.FadeVolume(0f, 0.5f);
                    BGMRepository.MainP4.FadeVolume(0f, 0.5f);
                }).AddTo(_disposable);
            }
        }

        public void Tick()
        {
            var playerPositionY = _playerMovement.Position.y;

            if (playerPositionY >= _interactiveMusicData.P2PlayerPositionY)
            {
                if (!_isTriggeredP2)
                {
                    BGMRepository.MainP2.FadeVolume(1f, FadeTime);
                    _isTriggeredP2 = true;
                }
            }

            if (playerPositionY >= _interactiveMusicData.P3PlayerPositionY)
            {
                if (!_isTriggeredP3)
                {
                    BGMRepository.MainP3.FadeVolume(1f, FadeTime);
                    _isTriggeredP3 = true;
                }
            }

            if (playerPositionY >= _interactiveMusicData.P4PlayerPositionY)
            {
                if (!_isTriggeredP4)
                {
                    BGMRepository.MainP4.FadeVolume(1f, FadeTime);
                    _isTriggeredP4 = true;
                }
            }
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}