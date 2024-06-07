using Rogue.Scripts.Utils;
using UnityEngine;

namespace Rogue.Scripts.View.UI
{
    public sealed class GameTimeView : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI _timeText;

        public void SetCurrent(float time)
        {
            _timeText.text = time.ToTimeString();
        }
        
        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}