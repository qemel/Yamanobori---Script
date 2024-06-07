using System;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Rogue.Scripts.View.UI
{
    public sealed class SoundVolumeSlidersView : MonoBehaviour
    {
        [SerializeField] private Slider _bgmSlider;
        [SerializeField] private Slider _seSlider;

        public ReadOnlyReactiveProperty<float> BgmVolume => _bgmVolume;
        private readonly ReactiveProperty<float> _bgmVolume = new();
        public ReadOnlyReactiveProperty<float> SeVolume => _seVolume;
        private readonly ReactiveProperty<float> _seVolume = new();

        private void Awake()
        {
            _bgmSlider.onValueChanged.AddListener(SetBgmVolume);
            _seSlider.onValueChanged.AddListener(SetSeVolume);
        }

        public void SetBgmVolume(float value)
        {
            if (value < 0.0f || value > 1.0f) return;
            if (Math.Abs(_bgmVolume.CurrentValue - value) < 0.001f) return;
            
            _bgmVolume.Value = value;
            _bgmSlider.value = value;
        }

        public void SetSeVolume(float value)
        {
            if (value < 0.0f || value > 1.0f) return;
            if (Math.Abs(_seVolume.CurrentValue - value) < 0.001f) return;
            
            _seVolume.Value = value;
            _seSlider.value = value;
        }

        private void OnDestroy()
        {
            _bgmSlider.onValueChanged.RemoveListener(SetBgmVolume);
            _seSlider.onValueChanged.RemoveListener(SetSeVolume);
        }
    }
}