using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rogue.Scripts.View.UI
{
    public sealed class FOVSliderView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

        public Observable<float> Fov => _fov;
        private readonly Subject<float> _fov = new();

        private void Start()
        {
            _fov.AddTo(this);
            _slider.OnValueChangedAsObservable()
                .Subscribe(val => _fov.OnNext(val))
                .AddTo(this);
        }

        /// <summary>
        /// 見た目に反映
        /// </summary>
        /// <param name="value"></param>
        public void SetSlider(float value)
        {
            _slider.value = value;
        }

        public void SetText(int value)
        {
            _textMeshProUGUI.text = value.ToString();
        }
    }
}