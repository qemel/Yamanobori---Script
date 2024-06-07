using UnityEngine;
using UnityEngine.UI;

namespace Rogue.Scripts.View.UI
{
    public sealed class SettingScreenView : MonoBehaviour
    {
        [SerializeField] private Button _primarySelectButton;

        public void Activate()
        {
            gameObject.SetActive(true);
            var buttons = GetComponentsInChildren<ButtonBaseView>();
            foreach (var button in buttons)
            {
                if (button.transform.TryGetComponent(out RestartButtonView restart))
                {
                    restart.gameObject.SetActive(false);
                }
            }

            _primarySelectButton.Select();
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }
    }
}