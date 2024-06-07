using UnityEngine;

namespace Rogue.Scripts.Data
{
    [CreateAssetMenu(fileName = "CameraControlInfo", menuName = "CameraControlInfo")]
    public sealed class CameraControlInfo : ScriptableObject
    {
        [Header("Distance")]
        [SerializeField] private float _defaultDistance = 6f;
        [SerializeField] private float _minDistance = 0f;
        [SerializeField] private float _maxDistance = 10f;
        [SerializeField] private float _distanceMovementSpeed = 5f;
        [SerializeField] private float _distanceMovementSharpness = 10f;

        [Header("Framing")]
        [SerializeField] private Vector2 _followPointFraming = new(0f, 0f);
        [SerializeField] private float _followingSharpness = 10000f;

        [Header("Obstruction")]
        [SerializeField] private float _obstructionCheckRadius = 0.2f;
        [SerializeField] private LayerMask _obstructionLayers = -1;
        [SerializeField] private float _obstructionSharpness = 10000f;

        [Header("Rotation")]
        [SerializeField] private float _rotationSpeed = 1f;
        [SerializeField] private float _rotationSharpness = 10000f;
        [SerializeField] private float _maxVerticalAngle = 80f;

        public float DefaultDistance => _defaultDistance;
        public float MinDistance => _minDistance;
        public float MaxDistance => _maxDistance;
        public float DistanceMovementSpeed => _distanceMovementSpeed;
        public float DistanceMovementSharpness => _distanceMovementSharpness;
        public Vector2 FollowPointFraming => _followPointFraming;
        public float FollowingSharpness => _followingSharpness;
        public float ObstructionCheckRadius => _obstructionCheckRadius;
        public LayerMask ObstructionLayers => _obstructionLayers;
        public float ObstructionSharpness => _obstructionSharpness;
        public float RotationSpeed => _rotationSpeed;
        public float RotationSharpness => _rotationSharpness;
        public float MaxVerticalAngle => _maxVerticalAngle;
        private void OnValidate()
        {
            _defaultDistance = Mathf.Clamp(_defaultDistance, _minDistance, _maxDistance);
        }
    }
}