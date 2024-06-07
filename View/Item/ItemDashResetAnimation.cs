using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

namespace Rogue.Scripts.View.Item
{
    public sealed class ItemDashResetAnimation : MonoBehaviour
    {
        /// <summary>
        /// localPosition.yの最大値
        /// </summary>
        [SerializeField] private float _height;

        /// <summary>
        /// Tweenの1往復にかかる時間
        /// </summary>
        [SerializeField] private float _loopDuration;
        [SerializeField] private Ease _ease;
        [SerializeField] private float _rotateSpeed;

        private void Start()
        {
            var startPosition = transform.position;
            LMotion.Create(startPosition.y, startPosition.y + _height, _loopDuration / 2f)
                .WithLoops(-1, LoopType.Yoyo)
                .WithEase(_ease)
                .BindToLocalPositionY(transform)
                .AddTo(gameObject);
        }

        private void Update()
        {
            transform.Rotate(Vector3.up, 90 * Time.deltaTime * _rotateSpeed);
        }
    }
}