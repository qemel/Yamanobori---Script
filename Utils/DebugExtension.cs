using UnityEngine;

namespace Rogue.Scripts.Utils
{
    public static class DebugExtension
    {
        public static void DebugWireSphere(Vector3 position, float radius, Color color, float duration = 0f,
            bool depthTest = true)
        {
            Debug.DrawRay(position + Vector3.up * radius, Vector3.forward * radius, color, duration, depthTest);
            Debug.DrawRay(position + Vector3.up * radius, Vector3.back * radius, color, duration, depthTest);
            Debug.DrawRay(position + Vector3.up * radius, Vector3.right * radius, color, duration, depthTest);
            Debug.DrawRay(position + Vector3.up * radius, Vector3.left * radius, color, duration, depthTest);
            Debug.DrawRay(position + Vector3.up * radius, Vector3.up * radius, color, duration, depthTest);
            Debug.DrawRay(position + Vector3.up * radius, Vector3.down * radius, color, duration, depthTest);
        }
        
        public static string ToRichText(this bool value)
        {
            return value ? "<color=green>o</color>" : "<color=red>x</color>";
        }
    }
}