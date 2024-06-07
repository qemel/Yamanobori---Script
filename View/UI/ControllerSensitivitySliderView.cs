using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rogue.Scripts.View.UI
{
    public sealed class ControllerSensitivitySliderView : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TextMeshProUGUI _textMeshProUGUI;

        public Observable<float> Sensitivity => _sensitivity;
        private readonly Subject<float> _sensitivity = new();

        private void Start()
        {
            _sensitivity.AddTo(this);
            _slider.OnValueChangedAsObservable()
                .Subscribe(val => _sensitivity.OnNext(val))
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

        public void SetText(float value)
        {
            _textMeshProUGUI.text = value.ToString("0.00");
        }
    }
}