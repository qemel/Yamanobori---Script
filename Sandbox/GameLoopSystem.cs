using UnityEngine;
using VContainer.Unity;

public class GameLoopSystem : ITickable
{
    private Player _player;
    private GameOverUI _gameOverUI;
    private EnemySpawner _enemySpawner; // 新しく追加

    // PureC#のクラスはコンストラクタで依存関係を解決する
    public GameLoopSystem(Player player, GameOverUI gameOverUI, EnemySpawner enemySpawner)
    {
        _player = player;
        _gameOverUI = gameOverUI;
        _enemySpawner = enemySpawner; // 追加
    }

    private bool _isGameOver;
    private float _spawnDuration = 3f;
    private float _spawnTimer;

    public void Tick()
    {
        if (_player.Hp <= 0)
        {
            if (!_isGameOver)
            {
                _gameOverUI.Show();
                _isGameOver = true;
            }
        }

        _spawnTimer += Time.deltaTime;
        if (_spawnTimer >= _spawnDuration)
        {
            _enemySpawner.Spawn(new Vector3(Random.Range(-5f, 5f), 0, Random.Range(-5f, 5f)));
            _spawnTimer = 0;
        }
    }
}