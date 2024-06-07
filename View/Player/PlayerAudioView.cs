using AnnulusGames.LucidTools.Audio;
using Rogue.Scripts.Data.Repository;
using UnityEngine;

namespace Rogue.Scripts.View.Player
{
    public sealed class PlayerAudioView : MonoBehaviour
    {
        [SerializeField] private AudioClip _jumpSound;
        [SerializeField] private AudioClip _landSound;
        [SerializeField] private AudioClip _deathSound;
        [SerializeField] private AudioClip _deathHardcoreSound;
        [SerializeField] private AudioClip _reviveSound;
        [SerializeField] private AudioClip _dashSound;


        public void PlayJumpSound()
        {
            LucidAudio.PlaySE(_jumpSound).SetTimeSamples();
        }

        public void PlayHighJumpSound()
        {
            LucidAudio.PlaySE(_jumpSound).SetPitch(1.5f).SetTimeSamples();
        }

        public void PlayLandSound()
        {
            LucidAudio.PlaySE(_landSound).SetTimeSamples().SetVolume(0.3f);
        }

        public void PlayDeathSound()
        {
            if (SettingRepository.IsHardcoreMode)
            {
                LucidAudio.PlaySE(_deathHardcoreSound).SetTimeSamples();
            }
            else
            {
                LucidAudio.PlaySE(_deathSound).SetTimeSamples();
            }
        }

        public void PlayReviveSound()
        {
            LucidAudio.PlaySE(_reviveSound).SetTimeSamples();
        }

        public void PlayDashSound()
        {
            LucidAudio.PlaySE(_dashSound).SetTimeSamples();
        }
    }
}