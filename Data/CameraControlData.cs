using Rogue.Scripts.Utils;
using UnityEngine;

namespace Rogue.Scripts.Data
{
    public sealed class CameraControlData
    {
        public bool CanMove { get; set; } = true;
        public bool IsGameClear { get; set; }
        public Transform FollowTarget { get; set; }
        public Vector3 PlanarDirection { get; set; }
        public Vector3 CurrentFollowPosition { get; set; }

        /// <summary>
        /// 現在の回転
        /// </summary>
        /// <remarks>
        /// YとXが通常の逆になっていることに注意
        /// </remarks>
        public Vector2 CurrentRotation { get; set; }
        public float TargetDistance { get; set; }

        /// <summary>
        /// Whether the distance between the cameraView and the target is obstructed by an object.
        /// </summary>
        public bool IsDistanceObstructed { get; set; }
        public float CurrentDistance { get; set; }
        public float TargetVerticalAngle { get; set; }
        public RaycastHit[] Obstructions { get; set; } = new RaycastHit[32];
        public RaycastHit ObstructionHit { get; set; }
        public int ObstructionCount { get; set; }
        public float ObstructionTime { get; set; }
        
        public override string ToString()
        {
            return $"CanMove: {CanMove.ToRichText()}, " +
                   $"FollowTarget: {FollowTarget}, " +
                   $"PlanarDirection: {PlanarDirection}, " +
                   $"CurrentFollowPosition: {CurrentFollowPosition}, " +
                   $"CurrentRotation: {CurrentRotation}, " +
                   $"TargetDistance: {TargetDistance}, " +
                   $"IsDistanceObstructed: {IsDistanceObstructed.ToRichText()}, " +
                   $"CurrentDistance: {CurrentDistance}, " +
                   $"TargetVerticalAngle: {TargetVerticalAngle}, " +
                   $"Obstructions: {Obstructions}, " +
                   $"ObstructionHit: {ObstructionHit}, " +
                   $"ObstructionCount: {ObstructionCount}, " +
                   $"ObstructionTime: {ObstructionTime}, ";
        }
    }
}