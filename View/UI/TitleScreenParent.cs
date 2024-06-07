using System.Collections.Generic;
using System.Linq;
using Rogue.Scripts.Data;
using UnityEngine;

namespace Rogue.Scripts.View.UI
{
    public sealed class TitleScreenParent : MonoBehaviour
    {
        public IEnumerable<TitleScreenComponent> TitleScreenComponents => _titleScreenComponents;
        [SerializeField] private TitleScreenComponent[] _titleScreenComponents;

        public TitleScreenComponent Get(ScreenTitleName screenTitleName)
        {
            return _titleScreenComponents.FirstOrDefault(titleScreenComponent =>
                titleScreenComponent.ScreenTitleName == screenTitleName);
        }
    }
}