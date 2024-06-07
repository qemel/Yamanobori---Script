using Rogue.Scripts.View.Player;
using UnityEngine;

namespace Rogue.Scripts.System
{
    public sealed class PlayerMovement
    {
        private readonly PlayerMovementView _view;

        public PlayerMovement(PlayerMovementView view)
        {
            _view = view;
        }

        public Vector3 Position => _view.Position;
        public Vector3 Up => _view.transform.up;
        public Vector3 Forward => _view.transform.forward;
        public Vector3 Right => _view.transform.right;
        public Vector3 Back => -Forward;
        public Vector3 Left => -Right;
        public Quaternion Rotation => _view.transform.rotation;
        public Vector3 Velocity => _view.Velocity;

        public void SetVelocity(Vector3 velocity)
        {
            _view.SetVelocity(velocity);
        }

        public void AddForce(Vector3 force, ForceMode mode)
        {
            _view.AddForce(force, mode);
        }

        public void SetUseGravity(bool useGravity)
        {
            _view.SetUseGravity(useGravity);
        }

        public void SetFacingDirection(Vector3 direction)
        {
            _view.SetFacingDirection(direction);
        }

        public void SetPosition(Vector3 position)
        {
            _view.SetPosition(position);
        }

        public void Stop()
        {
            _view.Stop();
        }

        public void Resume()
        {
            _view.Resume();
        }
    }
}