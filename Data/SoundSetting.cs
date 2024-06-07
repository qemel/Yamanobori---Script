using System;
using AnnulusGames.LucidTools.Audio;
using R3;
using UnityEngine;

namespace Rogue.Scripts.Data
{
    public sealed class SoundSetting : IDisposable
    {
        public ReadOnlyReactiveProperty<float> BgmVolume => _bgmVolume;
        private readonly ReactiveProperty<float> _bgmVolume = new();
        public ReadOnlyReactiveProperty<float> SeVolume => _seVolume;
        private readonly ReactiveProperty<float> _seVolume = new();
        
        public SoundSetting(float bgmVolume, float seVolume)
        {
            _bgmVolume.Value = bgmVolume;
            _seVolume.Value = seVolume;
        }
        
        public void SetBgmVolume(float volume)
        {
            _bgmVolume.Value = volume;
            LucidAudio.BGMVolume = volume;
            PlayerPrefs.SetFloat("BGMVolume", volume);
        }

        public void SetSeVolume(float volume)
        {
            _seVolume.Value = volume;
            LucidAudio.SEVolume = volume;
            PlayerPrefs.SetFloat("SEVolume", volume);
        }

        public void Dispose()
        {
            _bgmVolume?.Dispose();
            _seVolume?.Dispose();
        }
    }
}