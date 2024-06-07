using System.Collections.Generic;
using UnityEngine;

namespace Rogue.Scripts.View.Item
{
    public sealed class ItemDashResetParent : MonoBehaviour
    {
        public IEnumerable<ItemDashResetView> Views => _views;
        private ItemDashResetView[] _views;
        
        private void Awake()
        {
            _views = GetComponentsInChildren<ItemDashResetView>();
        }
    }
}