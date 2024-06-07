using System;
using R3;

namespace Rogue.Scripts.Data
{
    public sealed class PlayerEntityData : IDisposable
    {
        /// <summary>
        /// 一度でも移動したか
        /// </summary>
        public bool IsMoved { get; set; }
        public bool IsAlive { get; private set; } = true;
        public bool IsDead => !IsAlive;
        
        public Observable<Unit> OnDead => _onDead;
        private readonly Subject<Unit> _onDead = new();
        
        public Observable<Unit> OnRevive => _onRevive;
        private readonly Subject<Unit> _onRevive = new();
        
        public void Kill()
        {
            IsAlive = false;
            _onDead.OnNext(Unit.Default);
        }
        
        public void Revive()
        {
            IsAlive = true;
            _onRevive.OnNext(Unit.Default);
        }

        public void Dispose()
        {
            _onDead.Dispose();
        }
    }
}
