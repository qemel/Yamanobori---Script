using R3;
using Rogue.Scripts.Data;
using UnityEngine;
using VContainer;

namespace Rogue.Scripts.View.UI
{
    public sealed class TitleScreenChangeButtonView : ButtonBaseView
    {
        public ScreenTitleName ScreenTitleName => _screenTitleName;
        [SerializeField] private ScreenTitleName _screenTitleName;
        [Inject] private readonly TitleScreenStateData _titleScreenStateData;


        protected override void PostStart()
        {
            OnPressed.Subscribe(_ => { _titleScreenStateData.SetCurrentScreen(_screenTitleName); })
                .AddTo(this);
        }
    }
}