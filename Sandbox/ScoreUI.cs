using UnityEngine;
using UnityEngine.UI;
using VContainer;

public class ScoreUI : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    private Score _score;

    [Inject]
    public void Init(Score score)
    {
        _score = score;
    }

    private void Update()
    {
        _scoreText.text = _score.Value.ToString();
    }
}