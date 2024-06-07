using DebugUI;
using Rogue.Scripts.System;
using UnityEngine;
using VContainer;

namespace Rogue.Scripts.View.Debugger
{
    public sealed class PlayerDebugMenu : DebugUIBuilderBase
    {
        [Inject] private readonly PlayerMovement _playerMovement;

        protected override void Configure(IDebugUIBuilder builder)
        {
            builder.AddButton("最終地点前に移動", () => _playerMovement.SetPosition(new Vector3(5, 200, 130)));
        }
    }
}