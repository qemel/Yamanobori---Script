using Rogue.Scripts.Data;
using Rogue.Scripts.Utils;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Rogue.Scripts.View.UI
{
    public sealed class StageClearScreenView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _gameTimeText;
        [SerializeField] private Button _retryButton;

        public void Init(GameTime gameTime)
        {
            _gameTimeText.text = gameTime.ValueSec.CurrentValue.ToTimeString();
        }

        public void Activate()
        {
            gameObject.SetActive(true);
            _retryButton.Select();
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}