using System;
using Cysharp.Threading.Tasks;
using R3;
using Rogue.Scripts.Data;
using Rogue.Scripts.View.Item;
using VContainer.Unity;

namespace Rogue.Scripts.System.EntryPoint
{
    public sealed class DashResetSystem : IStartable, IDisposable
    {
        private readonly PlayerMovementData _playerMovementData;
        private readonly ItemDashResetParent _itemDashResetParent;

        private readonly CompositeDisposable _disposable = new();

        public DashResetSystem(PlayerMovementData playerMovementData, ItemDashResetParent itemDashResetParent)
        {
            _playerMovementData = playerMovementData;
            _itemDashResetParent = itemDashResetParent;
        }

        public void Start()
        {
            foreach (var dashView in _itemDashResetParent.Views)
            {
                dashView.OnPicked.Subscribe(x =>
                {
                    if (!_playerMovementData.HasDashed) return;
                    
                    _playerMovementData.HasDashed = false;
                    dashView.PlayEffect();
                    dashView.DeactivateAndRevive().Forget();
                    
                }).AddTo(_disposable);
            }
        }
        
        public void Dispose()
        {
            _disposable?.Dispose();
        }
    }
}