using Cysharp.Threading.Tasks;
using LitMotion;
using LitMotion.Extensions;
using UnityEngine;

namespace Rogue.Scripts.View.UI
{
    public sealed class DeathScreenView : MonoBehaviour
    {
        [SerializeField] private float _duration;

        public async UniTask PlayDeathScreen()
        {
            gameObject.SetActive(true);
            
            await LMotion
                .Create(-1920f - 100f, -100f, _duration / 3)
                .WithEase(Ease.InCubic)
                .BindToLocalPositionX(transform);

            await LMotion
                .Create(-100f, 100f, _duration / 3)
                .WithEase(Ease.Linear)
                .BindToLocalPositionX(transform);

            await LMotion
                .Create(100f, 1920f + 100f, _duration / 3)
                .WithEase(Ease.OutCubic)
                .BindToLocalPositionX(transform);

            gameObject.SetActive(false);
        }
    }
}