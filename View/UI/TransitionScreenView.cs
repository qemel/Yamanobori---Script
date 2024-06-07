using System.Threading;
using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace Rogue.Scripts.View.UI
{
    public sealed class TransitionScreenView : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private Image _image;

        [Header("Parameters")]
        [SerializeField] private float _duration = 1f;

        private Material _material;
        private static readonly int ProgressKey = Shader.PropertyToID("_Progress");

        private void OnValidate()
        {
            _canvasGroup.alpha = 0;
        }

        public void Awake()
        {
            _canvasGroup.alpha = 0;
            _canvasGroup.interactable = false;
            _canvasGroup.blocksRaycasts = false;
            _material = _image.material;
        }

        public async UniTask ShowAsync(CancellationToken cancellationToken)
        {
            _canvasGroup.blocksRaycasts = true;

            await LMotion
                .Create(0f, 1f, _duration)
                // .BindToMaterialFloat(_material, ProgressKey)
                .BindToCanvasGroupAlpha(_canvasGroup)
                .AddTo(gameObject)
                .ToUniTask(cancellationToken);
            
            _canvasGroup.alpha = 1;
        }

        public async UniTask HideAsync(CancellationToken cancellationToken)
        {
            _canvasGroup.blocksRaycasts = false;

            await LMotion
                .Create(1f, 0f, _duration)
                // .BindToMaterialFloat(_material, ProgressKey)
                .BindToCanvasGroupAlpha(_canvasGroup)
                .AddTo(gameObject)
                .ToUniTask(cancellationToken);

            _canvasGroup.alpha = 0;
        }
    }
}