using UnityEngine;
using VContainer;

public class Coin : MonoBehaviour
{
    private Score _score;

    [Inject]
    public void Init(Score score)
    {
        _score = score;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Playerと衝突したら
        if (collision.gameObject.TryGetComponent<Player>(out var player))
        {
            _score.Add(10);
        }
    }
}