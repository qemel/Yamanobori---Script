using AnnulusGames.LucidTools.Audio;
using R3;
using Rogue.Scripts.View.Player;
using UnityEngine;

namespace Rogue.Scripts.View
{
    public sealed class CheckpointView : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _onPlayerTouchedParticles;
        [SerializeField] private AudioClip _checkPointSe;
        [SerializeField] private Material _flagMaterial;

        /// <summary>
        /// 順番を把握するためのID
        /// </summary>
        [SerializeField] private int _id;
        public int Id => _id;

        public Observable<Unit> OnPlayerTouched => _onPlayerTouched;
        private readonly Subject<Unit> _onPlayerTouched = new();

        public Vector3 Position => transform.position;

        /// <summary>
        /// リスポーン時のプレイヤーの向き
        /// </summary>
        public Vector3 PlayerDirection => _playerDirection;
        [SerializeField] private Vector3 _playerDirection = new(0, 0, 1f);

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerMovementView _))
            {
                _onPlayerTouched.OnNext(Unit.Default);
            }
        }

        public void OnCheckPointSaved()
        {
            _onPlayerTouchedParticles.Play();
            ColorUtility.TryParseHtmlString("#FC93A4", out var color);
            _flagMaterial.color = color;
            LucidAudio.PlaySE(_checkPointSe).SetTimeSamples();
        }

        private void OnDestroy()
        {
            _onPlayerTouched.Dispose();
        }
    }
}