using Rogue.Scripts.View.Player;
using UnityEngine;

namespace Rogue.Scripts.System
{
    public sealed class PlayerCameraMovement
    {
        private readonly PlayerCameraView _playerCameraView;

        public PlayerCameraMovement(PlayerCameraView playerCameraView)
        {
            _playerCameraView = playerCameraView;
        }

        public Vector3 Position => _playerCameraView.transform.position;
        public Vector3 Forward => _playerCameraView.transform.forward;
        public Vector3 Right => _playerCameraView.transform.right;
        public Vector3 Up => _playerCameraView.transform.up;
        public Vector3 LocalEuler => _playerCameraView.transform.localEulerAngles;
        public Quaternion Rotation => _playerCameraView.transform.rotation;

        public void SetPosition(Vector3 position)
        {
            _playerCameraView.SetPosition(position);
        }

        public void SetRotation(Quaternion rotation)
        {
            _playerCameraView.SetRotation(rotation);
        }
        
        public void RevoluteRotation(Quaternion rotation)
        {
            _playerCameraView.RevoluteRotation(rotation);
        }
    }
}