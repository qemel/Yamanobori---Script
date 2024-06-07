using Rogue.Scripts.Data;
using UnityEngine;
using VContainer.Unity;

namespace Rogue.Scripts.System.EntryPoint
{
    public sealed class PlayerMovementDataLoggingSystem : ITickable
    {
        private readonly PlayerMovementData _data;

        public PlayerMovementDataLoggingSystem(PlayerMovementData data)
        {
            _data = data;
        }

        public void Tick()
        {
            Debug.Log(_data.ToString());
        }
    }
}