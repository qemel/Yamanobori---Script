using System;
using R3;
using UnityEngine;
using UnityEngine.InputSystem;
using VContainer.Unity;

namespace Rogue.Scripts.System.EntryPoint
{
    public sealed class GameInputProvider : IInitializable, ITickable, IDisposable
    {
        private readonly PlayerInput _playerInput;

        public ReadOnlyReactiveProperty<bool> Jump => _jump;
        private readonly ReactiveProperty<bool> _jump = new();
        public ReadOnlyReactiveProperty<bool> Grabbing => _grabbing;
        private readonly ReactiveProperty<bool> _grabbing = new();
        public ReadOnlyReactiveProperty<bool> Dash => _dash;
        private readonly ReactiveProperty<bool> _dash = new();
        public ReadOnlyReactiveProperty<bool> Click => _click;
        private readonly ReactiveProperty<bool> _click = new();
        public ReadOnlyReactiveProperty<bool> Tab => _tab;
        private readonly ReactiveProperty<bool> _tab = new();

        public ReadOnlyReactiveProperty<Vector3> XZDirection => _planeMovementDirection;
        private readonly ReactiveProperty<Vector3> _planeMovementDirection = new();

        public ReadOnlyReactiveProperty<Vector2> CameraMovementDirectionOfPad => _cameraMovementDirectionOfPad;
        private readonly ReactiveProperty<Vector2> _cameraMovementDirectionOfPad = new();

        public Vector2 MouseLook { get; private set; }
        public Vector2 ControllerLook { get; private set; }

        private readonly CompositeDisposable _disposable = new(8);

        public GameInputProvider(PlayerInput playerInput)
        {
            _playerInput = playerInput;
        }

        public bool IsJumpPressed => _jump.Value;
        public bool IsDashPressed => _dash.Value;
        public bool IsGrabbing => _grabbing.Value;

        public float X => _planeMovementDirection.Value.x;
        public float Z => _planeMovementDirection.Value.z;


        public void Initialize()
        {
            _jump.Value = false;
            _planeMovementDirection.Value = Vector2.zero;

            _jump.AddTo(_disposable);
            _grabbing.AddTo(_disposable);
            _dash.AddTo(_disposable);
            _click.AddTo(_disposable);
            _tab.AddTo(_disposable);
            _planeMovementDirection.AddTo(_disposable);
            _cameraMovementDirectionOfPad.AddTo(_disposable);
        }

        public void Tick()
        {
            _jump.Value = _playerInput.actions["Jump"].triggered;
            _grabbing.Value = _playerInput.actions["Grabbing"].ReadValue<float>() > 0;
            _dash.Value = _playerInput.actions["Dash"].triggered;
            _click.Value = _playerInput.actions["Click"].triggered;
            _tab.Value = _playerInput.actions["Tab"].triggered;
            _planeMovementDirection.Value = new Vector3(
                _playerInput.actions["Move"].ReadValue<Vector2>().x,
                0,
                _playerInput.actions["Move"].ReadValue<Vector2>().y
            ).normalized;
            _cameraMovementDirectionOfPad.Value = new Vector2(
                _playerInput.actions["CameraMove"].ReadValue<Vector2>().x,
                _playerInput.actions["CameraMove"].ReadValue<Vector2>().y
            );

            MouseLook = _playerInput.actions["Pointer"].ReadValue<Vector2>();
            ControllerLook = _playerInput.actions["Look"].ReadValue<Vector2>();
        }

        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}