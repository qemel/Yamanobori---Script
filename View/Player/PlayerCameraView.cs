using UnityEngine;

namespace Rogue.Scripts.View.Player
{
    public sealed class PlayerCameraView : MonoBehaviour
    {
        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetRotation(Quaternion rotation)
        {
            transform.rotation = rotation;
        }
        
        public void RevoluteRotation(Quaternion rotation)
        {
            transform.rotation = rotation * transform.rotation;
        }
    }
}