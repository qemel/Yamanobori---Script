using System;
using R3;
using Rogue.Scripts.Data;
using Rogue.Scripts.Data.Repository;
using Rogue.Scripts.Utils;
using Rogue.Scripts.View.Player;
using Unity.Mathematics;
using UnityEngine;
using VContainer.Unity;

namespace Rogue.Scripts.System.EntryPoint
{
    public sealed class PlayerCameraControlSystem : IInitializable, IStartable,
        ILateTickable, IDisposable
    {
        private readonly PlayerCameraFollowPointView _followPointView;
        private readonly PlayerCameraMovement _cameraMovement;
        private readonly CameraControlData _data;
        private readonly CameraControlInfo _info;
        private readonly GameInputProvider _input;

        private readonly CompositeDisposable _disposable = new(2);

        public PlayerCameraControlSystem(PlayerCameraMovement cameraMovement, GameInputProvider input,
            CameraControlData data,
            PlayerCameraFollowPointView followPointView,
            CameraControlInfo info)
        {
            _cameraMovement = cameraMovement;
            _data = data;
            _followPointView = followPointView;
            _info = info;
            _input = input;
        }

        public void Initialize()
        {
            _data.CurrentDistance = _info.DefaultDistance;
            _data.TargetDistance = _info.DefaultDistance;
            _data.TargetVerticalAngle = _cameraMovement.LocalEuler.x;
            _data.PlanarDirection = _cameraMovement.Forward;
        }

        public void Start()
        {
            SetFollowTransform(_followPointView.Transform);
        }

        public void LateTick()
        {
            if (!_data.CanMove)
            {
                if (_data.IsGameClear)
                {
                    // プレイヤーを中心としてカメラを一定角度回転させる
                    var delta = Time.deltaTime;
                    var rot = new Vector2(1.5f, -1);
                    UpdateWithInput(delta, rot);

                    return;
                }
            }

            var deltaTime = Time.deltaTime;
            var look = _input.MouseLook;
            var controllerLook = _input.ControllerLook;
            if (look.sqrMagnitude < 1f && controllerLook.sqrMagnitude > 0.01f)
            {
                look = controllerLook * SettingRepository.ControllerSensitivity;
            }

            var rotationInput = new Vector2(look.x, look.y);

            UpdateWithInput(deltaTime, rotationInput);
        }

        private void SetFollowTransform(Transform t)
        {
            _data.FollowTarget = t;
            _data.PlanarDirection = _data.FollowTarget.forward;
            _data.CurrentFollowPosition = _data.FollowTarget.position;
        }

        private void UpdateWithInput(float deltaTime, Vector2 rotationInput)
        {
            if (_data.FollowTarget == null) return;

            var up = _data.FollowTarget.up;
            var rotationFromInput = Quaternion.Euler(up * (rotationInput.x * _info.RotationSpeed));
            _data.PlanarDirection = rotationFromInput * _data.PlanarDirection;
            _data.PlanarDirection = Vector3.Cross(up,
                Vector3.Cross(_data.PlanarDirection, up));
            var planarRot = Quaternion.LookRotation(_data.PlanarDirection, up);

            _data.TargetVerticalAngle -= rotationInput.y * _info.RotationSpeed;
            _data.TargetVerticalAngle = Mathf.Clamp(_data.TargetVerticalAngle, 0f, _info.MaxVerticalAngle);
            var verticalRot = Quaternion.Euler(_data.TargetVerticalAngle, 0, 0);
            var targetRotation = Quaternion.Slerp(_cameraMovement.Rotation, planarRot * verticalRot,
                1f - Mathf.Exp(-_info.RotationSharpness * deltaTime));

            // 回転を適用
            _cameraMovement.SetRotation(targetRotation);

            // 角度に応じて距離を変更する
            if (_data.IsDistanceObstructed)
            {
                _data.TargetDistance = _data.CurrentDistance;
            }

            var zoomProc = _input.CameraMovementDirectionOfPad.CurrentValue.y;
            _data.TargetDistance += zoomProc * _info.DistanceMovementSpeed;
            _data.TargetDistance = _data.TargetDistance.Clamp(_info.MinDistance, _info.MaxDistance);

            _data.CurrentFollowPosition = _data.CurrentFollowPosition.Lerp(_data.FollowTarget.position,
                1f - Mathf.Exp(-_info.FollowingSharpness * deltaTime));

            {
                var closestHit = new RaycastHit { distance = Mathf.Infinity };
                _data.ObstructionCount = Physics.SphereCastNonAlloc(_data.CurrentFollowPosition,
                    _info.ObstructionCheckRadius,
                    -_cameraMovement.Forward, _data.Obstructions, _data.TargetDistance,
                    _info.ObstructionLayers,
                    QueryTriggerInteraction.Ignore);

                // Todo: Implement IgnoredColliders?
                for (var i = 0; i < _data.ObstructionCount; i++)
                {
                    if (_data.Obstructions[i].distance < closestHit.distance && _data.Obstructions[i].distance > 0)
                    {
                        closestHit = _data.Obstructions[i];
                    }
                }

                if (closestHit.distance < Mathf.Infinity)
                {
                    _data.IsDistanceObstructed = true;
                    _data.CurrentDistance = _data.CurrentDistance.Lerp(closestHit.distance,
                        1 - Mathf.Exp(-_info.ObstructionSharpness * deltaTime));
                }
                else
                {
                    _data.IsDistanceObstructed = false;
                    _data.CurrentDistance = _data.CurrentDistance.Lerp(_data.TargetDistance,
                        1 - Mathf.Exp(-_info.DistanceMovementSharpness * deltaTime));
                }
            }

            var targetPosition =
                _data.CurrentFollowPosition - _cameraMovement.Forward * _data.CurrentDistance;

            // Handle framing
            targetPosition += _cameraMovement.Right * _info.FollowPointFraming.x;
            targetPosition += _cameraMovement.Up * _info.FollowPointFraming.y;

            // Apply position
            _cameraMovement.SetPosition(targetPosition);
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}