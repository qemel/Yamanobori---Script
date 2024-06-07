using Rogue.Scripts.Data;
using UnityEngine;

namespace Rogue.Scripts.View.UI
{
    public sealed class TitleScreenComponent : MonoBehaviour
    {
        public ScreenTitleName ScreenTitleName => _screenTitleName;
        [SerializeField] private ScreenTitleName _screenTitleName;
        
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}