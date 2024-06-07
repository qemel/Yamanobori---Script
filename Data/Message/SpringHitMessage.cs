using UnityEngine;

namespace Rogue.Scripts.Data.Message
{
    public sealed record SpringHitMessage(Vector3 Direction, float JumpPower);
}