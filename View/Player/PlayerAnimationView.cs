using IceMilkTea.StateMachine;
using LitMotion;
using LitMotion.Extensions;
using Rogue.Scripts.Data;
using UnityEngine;

namespace Rogue.Scripts.View.Player
{
    [RequireComponent(typeof(MeshFilter))]
    public sealed class PlayerAnimationView : MonoBehaviour
    {
        [SerializeField] private MeshFilter[] _idleMeshFilters;
        [SerializeField] private MeshFilter[] _runMeshFilters;

        [Header("Parameters")]
        [SerializeField] private float _idleAnimationInterval;
        [SerializeField] private float _runAnimationInterval;
        [SerializeField] private float _jumpScaleChangeDuration;
        [SerializeField] private float _jumpScaleChangeAmount;
        [SerializeField] private float _dashScaleChangeDuration;
        [SerializeField] private float _dashScaleChangeAmount;


        private MeshFilter _meshFilter;
        private AnimationType _state;

        private bool _stateInitialized;

        private ImtStateMachine<PlayerAnimationView> _stateMachine;

        private void Awake()
        {
            _meshFilter = GetComponent<MeshFilter>();

            _stateMachine = new ImtStateMachine<PlayerAnimationView>(this);
            _stateMachine.AddTransition<IdleState, RunState>((int)AnimationType.Run);
            _stateMachine.AddTransition<RunState, IdleState>((int)AnimationType.Idle);
            _stateMachine.AddTransition<JumpState, IdleState>((int)AnimationType.Idle);
            _stateMachine.AddTransition<JumpState, RunState>((int)AnimationType.Run);
            _stateMachine.AddTransition<DashState, IdleState>((int)AnimationType.Idle);
            _stateMachine.AddTransition<DashState, RunState>((int)AnimationType.Run);
            _stateMachine.AddAnyTransition<JumpState>((int)AnimationType.Jump);
            _stateMachine.AddAnyTransition<DashState>((int)AnimationType.Dash);

            _stateMachine.SetStartState<IdleState>();
        }

        private void Start()
        {
            _stateMachine.Update();
            _stateInitialized = true;
        }

        private void Update()
        {
            _stateMachine.Update();
        }

        public bool TrySetAnimationState(AnimationType animationType)
        {
            if (!_stateInitialized) return false;
            _stateMachine.SendEvent((int)animationType);
            return true;
        }

        public void Kill()
        {
            gameObject.SetActive(false);
        }

        public void Revive()
        {
            gameObject.SetActive(true);
        }

        private sealed class IdleState : ImtStateMachine<PlayerAnimationView, int>.State
        {
            private float _time;
            private int _index;

            protected internal override void Update()
            {
                _time += Time.deltaTime;
                if (_time >= Context._idleAnimationInterval)
                {
                    _time = 0;
                    _index = (_index + 1) % Context._idleMeshFilters.Length;
                    Context._meshFilter.mesh = Context._idleMeshFilters[_index].sharedMesh;
                }
            }
        }

        private sealed class RunState : ImtStateMachine<PlayerAnimationView, int>.State
        {
            private float _time;
            private int _index;

            protected internal override void Update()
            {
                _time += Time.deltaTime;
                if (_time >= Context._runAnimationInterval)
                {
                    _time = 0;
                    _index = (_index + 1) % Context._runMeshFilters.Length;
                    Context._meshFilter.mesh = Context._runMeshFilters[_index].sharedMesh;
                }
            }
        }

        private sealed class JumpState : ImtStateMachine<PlayerAnimationView, int>.State
        {
            protected internal override void Enter()
            {
                LMotion
                    .Create(1f, 1 / Context._jumpScaleChangeAmount, Context._jumpScaleChangeDuration)
                    .WithEase(Ease.InOutCubic)
                    .WithLoops(2, LoopType.Yoyo)
                    .BindToLocalScaleX(Context.transform);

                LMotion
                    .Create(1f, Context._jumpScaleChangeAmount, Context._jumpScaleChangeDuration)
                    .WithEase(Ease.InOutCubic)
                    .WithLoops(2, LoopType.Yoyo)
                    .BindToLocalScaleY(Context.transform);

                LMotion
                    .Create(1f, 1 / Context._jumpScaleChangeAmount, Context._jumpScaleChangeDuration)
                    .WithEase(Ease.InOutCubic)
                    .WithLoops(2, LoopType.Yoyo)
                    .BindToLocalScaleZ(Context.transform);
            }
        }

        private sealed class DashState : ImtStateMachine<PlayerAnimationView, int>.State
        {
            protected internal override void Enter()
            {
                LMotion
                    .Create(1f, Context._dashScaleChangeAmount, Context._dashScaleChangeDuration)
                    .WithEase(Ease.InOutCubic)
                    .WithLoops(2, LoopType.Yoyo)
                    .BindToLocalScaleX(Context.transform);

                LMotion
                    .Create(1f, 1 / Context._dashScaleChangeAmount, Context._dashScaleChangeDuration)
                    .WithEase(Ease.InOutCubic)
                    .WithLoops(2, LoopType.Yoyo)
                    .BindToLocalScaleY(Context.transform);

                LMotion
                    .Create(1f, Context._dashScaleChangeAmount, Context._dashScaleChangeDuration)
                    .WithEase(Ease.InOutCubic)
                    .WithLoops(2, LoopType.Yoyo)
                    .BindToLocalScaleZ(Context.transform);
            }
        }
    }
}