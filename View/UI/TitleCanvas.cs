using UnityEngine;
using UnityEngine.UI;

namespace Rogue.Scripts.View.UI
{
    public sealed class TitleCanvas : MonoBehaviour
    {
        [SerializeField] private Button _initialSelectButton;
        
        private void Awake()
        {
            _initialSelectButton.Select();
        }
    }
}