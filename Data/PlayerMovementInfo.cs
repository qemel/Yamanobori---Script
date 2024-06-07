using UnityEngine;

namespace Rogue.Scripts.Data
{
    [CreateAssetMenu(fileName = "NewPlayerMovementInfo", menuName = "PlayerMovementInfo")]
    public sealed class PlayerMovementInfo : ScriptableObject
    {
        [Header("Looking")]
        [SerializeField] private float _turnSpeed = 2;

        [Header("Detection")]
        [SerializeField] private LayerMask _solidMask;
        [SerializeField] private float _grounderRadius = 0.2f;
        [SerializeField] private float _grounderOffset = 0f;
        [SerializeField] private float _wallDetectionDistance = 0.5f;
        [SerializeField] private float _wallDetectionRadius = 0.38f;

        [Header("Walk")]
        [SerializeField] private float _deadZone = 0.1f;
        [SerializeField] private float _runSpeed = 8;
        [SerializeField] private float _runAcceleration = 2;
        [SerializeField] private float _minWalkingPenalty = 0.5f;
        [SerializeField] private float _movementLerpSpeed = 100;

        [Header("Jump")]
        [SerializeField] private float _wallJumpMovementLerpSpeed = 20;
        [SerializeField] private float _jumpForce = 15;
        [SerializeField] private float _fallMultiplier = 7;
        [SerializeField] private float _addExtraJumpFallThreshold = 8;
        [SerializeField] private float _wallJumpLockTime = 0.25f;
        [SerializeField] private float _wallJumpMultiplier = 1.2f;
        [SerializeField] private float _coyoteTime = 0.3f;
        [SerializeField] private bool _isDoubleJumpEnabled = true;

        [Header("Wall Slide")]
        [SerializeField] private float _slideSpeed = 2;
        [SerializeField] private float _wallClamberOffset = 2f;
        [SerializeField] private float _wallClamberPower = 5;

        [Header("Dash")]
        [SerializeField] private LayerMask _dashTargetMask;
        [SerializeField] private float _dashSpeed = 30;
        [SerializeField] private float _dashLength = 0.2f;
        [SerializeField] private float _dashAngle;
        [SerializeField] private float _dashTargetCastRadius = 4;
        [SerializeField] private float _dashTargetCastExtent = 6;
        [SerializeField] private float _dashTargetCastDistance = 15;
        [SerializeField] private bool _useDashTargets = true;

        [Header("Acceleration")]
        [SerializeField] private float _dashJumpBonusAcceleration = 2;
        [SerializeField] private float _dashJumpBonusMinSpeed = 10;

        [Header("Fall")]
        [SerializeField] private float _wallUpSpeedMax = 10;
        

        public float TurnSpeed => _turnSpeed;
        
        /// <summary>
        /// 地面や壁を検出するためのマスク
        /// </summary>
        public LayerMask SolidMask => _solidMask;
        public float GrounderRadius => _grounderRadius;
        public float GrounderOffset => _grounderOffset;
        public float WallDetectionDistance => _wallDetectionDistance;
        public float WallDetectionRadius => _wallDetectionRadius;
        public float DeadZone => _deadZone;
        public float RunSpeed => _runSpeed;
        public float RunAcceleration => _runAcceleration;
        public float MinWalkingPenalty => _minWalkingPenalty;
        public float MovementLerpSpeed => _movementLerpSpeed;
        public float WallJumpMovementLerpSpeed => _wallJumpMovementLerpSpeed;
        public float JumpForce => _jumpForce;
        public float FallMultiplier => _fallMultiplier;

        /// <summary>
        /// ジャンプの追加速度が発生するY軸速さの閾値 
        /// </summary>
        public float AddExtraJumpFallThreshold => _addExtraJumpFallThreshold;
        public float WallJumpLockTime => _wallJumpLockTime;
        public float WallJumpMultiplier => _wallJumpMultiplier;
        public float CoyoteTime => _coyoteTime;
        public bool IsDoubleJumpEnabled => _isDoubleJumpEnabled;
        public float SlideSpeed => _slideSpeed;
        public float WallClamberOffset => _wallClamberOffset;
        public float WallClamberPower => _wallClamberPower;
        public LayerMask DashTargetMask => _dashTargetMask;
        public float DashSpeed => _dashSpeed;
        public float DashLength => _dashLength;
        public float DashAngle => _dashAngle;
        public float DashTargetCastRadius => _dashTargetCastRadius;
        public float DashTargetCastExtent => _dashTargetCastExtent;
        public float DashTargetCastDistance => _dashTargetCastDistance;
        public bool UseDashTargets => _useDashTargets;
        public float DashJumpBonusAcceleration => _dashJumpBonusAcceleration;
        public float DashJumpBonusMinSpeed => _dashJumpBonusMinSpeed;
        public float WallUpSpeedMax => _wallUpSpeedMax;
    }
}