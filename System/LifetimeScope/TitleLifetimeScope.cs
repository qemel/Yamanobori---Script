using Rogue.Scripts.Data;
using Rogue.Scripts.System.EntryPoint.Presenter;
using Rogue.Scripts.View.UI;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace Rogue.Scripts.System.LifetimeScope
{
    public sealed class TitleLifetimeScope : VContainer.Unity.LifetimeScope
    {
        [SerializeField] private GameStartButtonView _gameStartButtonView;
        [SerializeField] private GameHardcoreStartButtonView _gameHardcoreStartButtonView;
        [SerializeField] private TransitionScreenView _transitionScreenView;
        [SerializeField] private SoundVolumeSlidersView _soundVolumeSlidersView;
        [SerializeField] private TitleScreenParent _titleScreenParent;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.UseEntryPoints(entry =>
            {
                entry.Add<TitleUIPresenter>();
                entry.Add<SoundVolumePresenter>();
                entry.Add<TitleScreenPresenter>();
            });

            builder.Register<SceneLoader>(Lifetime.Singleton);
            builder.Register<TransitionScreen>(Lifetime.Singleton);
            builder.Register<TitleScreenStateData>(Lifetime.Singleton);

            builder.RegisterComponent(_gameStartButtonView);
            builder.RegisterComponent(_gameHardcoreStartButtonView);
            builder.RegisterComponent(_transitionScreenView);
            builder.RegisterComponent(_soundVolumeSlidersView);
            builder.RegisterComponent(_titleScreenParent);
        }
    }
}