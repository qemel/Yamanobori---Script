using UnityEngine;

namespace Rogue.Scripts.View.UI
{
    public sealed class TutorialDialogView : MonoBehaviour
    {
        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }
    }
}