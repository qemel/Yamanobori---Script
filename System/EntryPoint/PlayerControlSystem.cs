using System;
using MessagePipe;
using R3;
using Rogue.Scripts.Data;
using Rogue.Scripts.Data.Message;
using Rogue.Scripts.Utils;
using Rogue.Scripts.View.Player;
using UnityEngine;
using VContainer.Unity;

namespace Rogue.Scripts.System.EntryPoint
{
    public sealed class PlayerControlSystem : IStartable, ITickable, IDisposable
    {
        private readonly PlayerMovementData _data;
        private readonly PlayerMovement _movement;
        private readonly PlayerAnimationView _animationView;
        private readonly PlayerEffect _effect;
        private readonly PlayerAudioView _audioView;
        private readonly GameInputProvider _input;
        private readonly PlayerMovementInfo _movementInfo;
        private readonly PlayerCameraMovement _cameraMovement;
        private readonly PlayerEntityData _entityData;
        private readonly ISubscriber<SpringHitMessage> _springHitSubscriber;

        private readonly Collider[] _ground = new Collider[1];
        private readonly Collider[] _wall = new Collider[1];

        private readonly CompositeDisposable _disposable = new();

        public PlayerControlSystem(GameInputProvider input, PlayerAnimationView animationView,
            PlayerMovementData data, PlayerMovementInfo movementInfo, PlayerEffect effect,
            PlayerCameraMovement cameraMovement,
            PlayerMovement movement,
            ISubscriber<SpringHitMessage> springHitSubscriber, PlayerAudioView audioView, PlayerEntityData entityData)
        {
            _input = input;
            _animationView = animationView;
            _data = data;
            _movementInfo = movementInfo;
            _effect = effect;
            _cameraMovement = cameraMovement;
            _movement = movement;
            _springHitSubscriber = springHitSubscriber;
            _audioView = audioView;
            _entityData = entityData;
        }

        private Vector3 WallDetectPosition =>
            _movement.Position + Vector3.up + _movement.Forward * _movementInfo.WallDetectionDistance;

        public void Start()
        {
            _springHitSubscriber.Subscribe(message =>
            {
                _movement.SetVelocity(new Vector3(_movement.Velocity.x, message.JumpPower, _movement.Velocity.z));
                _data.HasDashed = false;
                _animationView.TrySetAnimationState(AnimationType.Jump);
                _effect.PlayJumpEffect();
                _audioView.PlayJumpSound();
            }).AddTo(_disposable);
        }

        public void Tick()
        {
            if (AnyButtonPressed())
            {
                _entityData.IsMoved = true;
            }
            
            if (!_data.CanMove)
            {
                _animationView.TrySetAnimationState(AnimationType.Idle);
                _movement.SetVelocity(Vector3.zero);
                return;
            }

            if (_entityData.IsDead) return;

            HandleFacingDirection();
            HandleGrounding();
            HandleRun();
            HandleJumpAndFall();
            HandleDash();
        }

        /// <summary>
        /// Handles the facing direction of the player.
        /// </summary>
        private void HandleFacingDirection()
        {
            var direction = GetXZDirectionFromCamera();

            if (direction != Vector3.zero)
            {
                _movement.SetFacingDirection(direction);
            }
        }

        private void HandleGrounding()
        {
            var groundedThisFrame = Physics.OverlapSphereNonAlloc(
                _movement.Position + new Vector3(0, _movementInfo.GrounderOffset),
                _movementInfo.GrounderRadius, _ground, _movementInfo.SolidMask) > 0;

            if (groundedThisFrame && !_data.IsGrounded)
            {
                _audioView.PlayLandSound();
                _data.HasDashed = false;
                _data.HasJumped = false;
            }

            if (!groundedThisFrame && _data.IsGrounded)
            {
                _data.TimeLastLeftGround = Time.time;
            }

            if (groundedThisFrame)
            {
                _data.IsGrounded = true;
                _data.CurrentMovementLerpSpeed = 100;
            }
            else
            {
                _data.IsGrounded = false;
            }

            // Wall detection
            _data.IsAgainstWall = Physics.OverlapSphereNonAlloc(WallDetectPosition, _movementInfo.WallDetectionRadius,
                _wall, _movementInfo.SolidMask) > 0;

            // idle detection
            var isIdle = _input.XZDirection.CurrentValue == Vector3.zero && _data.IsGrounded;
            if (isIdle)
            {
                _animationView.TrySetAnimationState(AnimationType.Idle);
            }
        }

        private void HandleRun()
        {
            if (_data.IsDashingUp) return;

            var direction = GetXZDirectionFromCamera();

            _data.CurrentMovementLerpSpeed =
                Mathf.MoveTowards(_data.CurrentMovementLerpSpeed, 100,
                    _movementInfo.WallJumpMovementLerpSpeed * Time.deltaTime);

            var isStopping = direction == Vector3.zero;

            if (isStopping)
            {
                _data.CurrentWalkingPenalty -= _movementInfo.RunAcceleration * Time.deltaTime;
            }
            else
            {
                _data.CurrentWalkingPenalty += _movementInfo.RunAcceleration * Time.deltaTime;
                _animationView.TrySetAnimationState(AnimationType.Run);
            }

            _data.CurrentWalkingPenalty = _data.CurrentWalkingPenalty.Clamp(_movementInfo.MinWalkingPenalty, 1);

            // Set current y velocity and add walking penalty
            var velocity = _movement.Velocity;
            var targetVel = new Vector3(direction.x, velocity.y, direction.z) *
                            _data.CurrentWalkingPenalty * _movementInfo.RunSpeed;

            var idealVel = new Vector3(targetVel.x, velocity.y, targetVel.z);
            _movement.SetVelocity(Vector3.MoveTowards(_movement.Velocity, idealVel,
                _data.CurrentMovementLerpSpeed * Time.deltaTime));
        }

        private void HandleJumpAndFall()
        {
            if (_input.IsJumpPressed)
            {
                var isCoyoteTime = Time.time < _data.TimeLastLeftGround + _movementInfo.CoyoteTime;

                // もしジャンプしてない状態でコヨーテタイムを超えた落下をしていた場合、1回ジャンプしたことにする
                if (!_data.IsGrounded && !isCoyoteTime && !_data.HasJumped)
                {
                    _data.HasJumped = true;
                }

                var isDoubleJumping = _movementInfo.IsDoubleJumpEnabled && _data.HasJumped && !_data.HasDoubleJumped;

                if (!_data.IsGrounded && _data.IsAgainstWall)
                {
                    _data.CurrentMovementLerpSpeed = _movementInfo.WallJumpMovementLerpSpeed;

                    if (GetWallHitAroundPlayer(out var wallHit))
                    {
                        // Wall jump
                        ExecuteJump(new Vector3(
                                _movement.Velocity.x + wallHit.normal.x * _movementInfo.JumpForce,
                                _movementInfo.JumpForce * _movementInfo.WallJumpMultiplier,
                                _movement.Velocity.z + wallHit.normal.z * _movementInfo.JumpForce
                            )
                        );
                        _animationView.TrySetAnimationState(AnimationType.Jump);
                        _effect.PlayJumpEffect();
                        _audioView.PlayJumpSound();

                        if (_input.XZDirection.CurrentValue == Vector3.zero)
                        {
                            _movement.SetFacingDirection(wallHit.normal);
                        }
                    }
                }
                else if (_data.IsGrounded || isCoyoteTime || isDoubleJumping)
                {
                    if (!_data.HasJumped || isDoubleJumping)
                    {
                        if (_movement.Velocity.X0Z().magnitude < _movementInfo.DashJumpBonusMinSpeed)
                        {
                            ExecuteJump(_movement.Velocity.X0Z() + Vector3.up * _movementInfo.JumpForce,
                                _data.HasJumped);
                            _audioView.PlayJumpSound();
                        }
                        else
                        {
                            ExecuteJump(
                                _movement.Velocity.X0Z()
                                + Vector3.up * _movementInfo.JumpForce
                                + _movement.Forward * _movementInfo.DashJumpBonusAcceleration
                                + _movement.Up * _movementInfo.DashJumpBonusAcceleration * 0.5f
                            );
                            _data.HasDashed = false;
                            _audioView.PlayHighJumpSound();
                        }

                        _animationView.TrySetAnimationState(AnimationType.Jump);
                        _effect.PlayJumpEffect();
                    }
                }
            }

            // Falling
            if (_movement.Velocity.y < _movementInfo.AddExtraJumpFallThreshold ||
                _movement.Velocity.y > 0 && !_input.IsJumpPressed)
            {
                _movement.SetVelocity(_movement.Velocity +
                                      _movementInfo.FallMultiplier * Physics.gravity.y * Vector3.up * Time.deltaTime);
            }

            return;

            void ExecuteJump(Vector3 dir, bool doubleJump = false)
            {
                _movement.SetVelocity(dir);
                _data.HasDoubleJumped = doubleJump;
                _data.HasJumped = true;
            }
        }

        private void HandleDash()
        {
            if (_movement.Velocity.y < 0)
            {
                _data.IsDashingUp = false;
            }

            if (!_input.IsDashPressed) return;
            if (_data.HasDashed) return;

            var forward = _movement.Forward;
            _data.DashDirection =
                new Vector3(forward.x, Mathf.Tan(_movementInfo.DashAngle.ToRadians()), forward.z);

            _data.HasDashed = true;
            _data.IsDashingUp = true;
            _movement.SetUseGravity(false);
            _data.TimeLastDashed = Time.time;
            _movement.SetVelocity(_data.DashDirection * _movementInfo.DashSpeed);
            _animationView.TrySetAnimationState(AnimationType.Dash);
            _audioView.PlayDashSound();
            _effect.PlayDashEffect(_movement.Rotation);
            _movement.SetUseGravity(true);
        }

        private bool GetWallHit(out RaycastHit outHit)
        {
            if (Physics.Raycast(_movement.Position + Vector3.up,
                    _movement.Forward, out var hit, 1.2f, _movementInfo.SolidMask))
            {
                outHit = hit;
                return true;
            }

            outHit = new RaycastHit();
            return false;
        }

        private bool GetWallHitAroundPlayer(out RaycastHit outHit)
        {
            if (Physics.Raycast(_movement.Position + Vector3.up,
                    _movement.Forward, out var hit, 1.2f, _movementInfo.SolidMask))
            {
                outHit = hit;
                return true;
            }

            if (Physics.Raycast(_movement.Position + Vector3.up,
                    _movement.Forward + _movement.Right, out var hitRight, 1.2f, _movementInfo.SolidMask))
            {
                outHit = hitRight;
                return true;
            }

            if (Physics.Raycast(_movement.Position + Vector3.up,
                    _movement.Left, out var hitLeft, 1.2f, _movementInfo.SolidMask))
            {
                outHit = hitLeft;
                return true;
            }

            if (Physics.Raycast(_movement.Position + Vector3.up,
                    _movement.Back, out var hitBack, 1.2f, _movementInfo.SolidMask))
            {
                outHit = hitRight;
                return true;
            }

            outHit = new RaycastHit();
            return false;
        }

        /// <summary>
        /// Cameraから見たXZ方向の入力を返す
        /// </summary>
        /// <returns></returns>
        private Vector3 GetXZDirectionFromCamera()
        {
            if (_input.X * _input.X + _input.Z * _input.Z < _movementInfo.DeadZone * _movementInfo.DeadZone)
            {
                return Vector3.zero;
            }

            var cameraForward = Vector3.Scale(_cameraMovement.Forward, new Vector3(1, 0, 1)).normalized;
            var cameraRight = Vector3.Scale(_cameraMovement.Right, new Vector3(1, 0, 1)).normalized;

            return (cameraForward * _input.Z + cameraRight * _input.X).normalized;
        }
        
        private bool AnyButtonPressed()
        {
            return _input.X != 0 || _input.Z != 0 || _input.IsJumpPressed || _input.IsDashPressed;
        }

        public void Dispose()
        {
            _input?.Dispose();
            _disposable?.Dispose();
        }
    }
}