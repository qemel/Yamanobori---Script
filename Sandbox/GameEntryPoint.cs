using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameEntryPoint : VContainer.Unity.LifetimeScope
{
    [SerializeField] private Player _player;
    [SerializeField] private Enemy _enemy;
    [SerializeField] private GameOverUI _gameOverUI;
    [SerializeField] private ScoreUI _scoreUI;

    protected override void Configure(IContainerBuilder builder)
    {
        // DIするクラス（コンストラクタの引数になってるクラス）を登録していく
        
        // PureC#
        builder.Register<Score>(Lifetime.Singleton);
        builder.Register<EnemySpawner>(Lifetime.Singleton);
        builder.RegisterEntryPoint<GameLoopSystem>(Lifetime.Singleton);

        // MonoBehaviour
        builder.RegisterComponent(_enemy);
        builder.RegisterComponent(_player);
        builder.RegisterComponent(_gameOverUI);
        builder.RegisterComponent(_scoreUI);
    }
}