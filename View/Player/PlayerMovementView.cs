using UnityEngine;

namespace Rogue.Scripts.View.Player
{
    public sealed class PlayerMovementView : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;

        public Vector3 Position => transform.position;
        public Vector3 Velocity => _rigidbody.velocity;

        private bool _isAvailable = true;

        public void SetVelocity(Vector3 velocity)
        {
            if (!_isAvailable) return;
            _rigidbody.velocity = velocity;
        }

        public void AddForce(Vector3 force, ForceMode forceMode)
        {
            if (!_isAvailable) return;
            _rigidbody.AddForce(force, forceMode);
        }

        public void SetUseGravity(bool useGravity)
        {
            if (!_isAvailable) return;
            _rigidbody.useGravity = useGravity;
        }

        public void SetFacingDirection(Vector3 direction)
        {
            if (!_isAvailable) return;
            transform.forward = direction;
        }

        public void SetPosition(Vector3 position)
        {
            if (!_isAvailable) return;
            transform.position = position;
        }

        public void Stop()
        {
            SetUseGravity(false);
            _rigidbody.velocity = Vector3.zero;
            _isAvailable = false;
        }

        public void Resume()
        {
            _isAvailable = true;
            SetUseGravity(true);
        }
    }
}