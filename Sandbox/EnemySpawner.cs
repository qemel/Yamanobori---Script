using UnityEngine;

public class EnemySpawner
{
    private Enemy _enemyPrefab;
    private Score _score;

    public EnemySpawner(Enemy enemyPrefab, Score score)
    {
        _enemyPrefab = enemyPrefab; // PrefabをDI
        _score = score; // スポーン用にScoreをDI
    }

    public void Spawn(Vector3 position)
    {
        var enemy = Object.Instantiate(_enemyPrefab, position, Quaternion.identity);
        enemy.Init(_score);
    }
}