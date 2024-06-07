using UnityEngine;

namespace Rogue.Scripts.View
{
    public sealed class SolidParent : MonoBehaviour
    {
        private void Awake()
        {
            foreach (Transform child in transform)
            {
                if (child.gameObject.layer != gameObject.layer)
                {
                    Debug.LogWarning($"Child object {child.name} has a different layer than the parent object {name}.",
                        child);
                }
            }
        }
    }
}