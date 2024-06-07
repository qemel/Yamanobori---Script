using System;
using System.Threading;
using AnnulusGames.LucidTools.Audio;
using Cysharp.Threading.Tasks;
using LitMotion;
using R3;
using Rogue.Scripts.View.Player;
using UnityEngine;

namespace Rogue.Scripts.View.Item
{
    public sealed class ItemDashResetView : MonoBehaviour
    {
        [SerializeField] private MeshRenderer _meshRenderer;
        [SerializeField] private Outline _outline;
        
        [SerializeField] private float _reviveTime;
        [SerializeField] private AudioClip _onGetSound;
        [SerializeField] private ParticleSystem _particle;

        public Observable<Unit> OnPicked => _onPicked;
        private readonly Subject<Unit> _onPicked = new();

        private Color _initialColor;

        private CancellationToken _cancellationToken;

        private void Awake()
        {
            _cancellationToken = this.GetCancellationTokenOnDestroy();
            _initialColor = _meshRenderer.material.color;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovementView _))
            {
                _onPicked.OnNext(Unit.Default);
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovementView _))
            {
                _onPicked.OnNext(Unit.Default);
            }
        }

        public void PlayEffect()
        {
            Instantiate(_particle, transform.position, Quaternion.identity).Play();
            LucidAudio.PlaySE(_onGetSound).SetTimeSamples();
        }

        public async UniTask DeactivateAndRevive()
        {
            gameObject.SetActive(false);
            _outline.enabled = false;
            await UniTask.Delay((int)(_reviveTime * 1000), cancellationToken: _cancellationToken);
            gameObject.SetActive(true);
            _meshRenderer.material.color = new Color(_initialColor.r, _initialColor.g, _initialColor.b, 0f);
            await LMotion
                .Create(0f, 1f, 1f)
                .WithEase(Ease.InSine)
                .Bind(a => _meshRenderer.material.color =
                    new Color(_initialColor.r, _initialColor.g, _initialColor.b, a))
                .ToUniTask(cancellationToken: _cancellationToken);
            _outline.enabled = true;
        }
    }
}