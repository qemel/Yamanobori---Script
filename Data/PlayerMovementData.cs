using Rogue.Scripts.Utils;
using UnityEngine;

namespace Rogue.Scripts.Data
{
    public sealed class PlayerMovementData
    {
        public bool CanMove { get; set; } = true;
        public bool IsGrounded { get; set; }
        public bool IsAgainstWall { get; set; }
        public bool HasJumped { get; set; }
        public bool IsDashingUp { get; set; }

        /// <summary>
        /// ダッシュしてから地面に着地するまでtrue
        /// </summary>
        public bool HasDashed { get; set; }
        public bool HasDoubleJumped { get; set; }
        public float CurrentMovementLerpSpeed { get; set; }
        public float CurrentWalkingPenalty { get; set; }
        public float TimeLastDashed { get; set; }
        public float TimeLastLeftGround { get; set; }
        public Vector3 DashDirection { get; set; }

        public override string ToString()
        {
            return $"IsGrounded: {IsGrounded.ToRichText()}, " +
                   $"IsAgainstWall: {IsAgainstWall.ToRichText()}, " +
                   $"HasJumped: {HasJumped.ToRichText()}, " +
                   $"IsDashingUp: {IsDashingUp.ToRichText()}, " +
                   $"HasDashed: {HasDashed.ToRichText()}, " +
                   $"HasDoubleJumped: {HasDoubleJumped.ToRichText()}, " +
                   $"CurrentMovementLerpSpeed: {CurrentMovementLerpSpeed}, " +
                   $"CurrentWalkingPenalty: {CurrentWalkingPenalty}, " +
                   $"TimeLastDashed: {TimeLastDashed}, " +
                   $"TimeLastLeftGround: {TimeLastLeftGround}, " +
                   $"DashDirection: {DashDirection}";
        }
    }
}