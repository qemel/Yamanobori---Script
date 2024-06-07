using AnnulusGames.LucidTools.Audio;
using R3;
using UnityEngine;

namespace Rogue.Scripts.View.UI
{
    public sealed class GameStartButtonView : ButtonBaseView
    {
        [SerializeField] private AudioClip _startSound;

        protected override void PostStart()
        {
            OnPressed.Subscribe(_ => { LucidAudio.PlaySE(_startSound).SetTimeSamples(); }).AddTo(this);
        }
    }
}