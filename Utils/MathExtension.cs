using System;
using UnityEngine;
using Vector3 = UnityEngine.Vector3;

namespace Rogue.Scripts.Utils
{
    public static class MathExtension
    {
        public static float Clamp(this float value, float min, float max)
        {
            return Math.Clamp(value, min, max);
        }

        public static int Clamp(this int value, int min, int max)
        {
            return Math.Clamp(value, min, max);
        }

        public static float Lerp(this float a, float b, float t)
        {
            return a + (b - a) * t;
        }

        public static Vector3 Lerp(this Vector3 a, Vector3 b, float t)
        {
            return a + (b - a) * t;
        }

        public static float ToRadians(this float degrees)
        {
            return degrees * Mathf.Deg2Rad;
        }

        public static float ToDegrees(this float radians)
        {
            return radians * Mathf.Rad2Deg;
        }

        /// <summary>
        /// currentからtargetへdelta分だけ移動させた座標を返す
        /// </summary>
        /// <param name="current"></param>
        /// <param name="target"></param>
        /// <param name="delta">移動距離</param>
        /// <remarks>UnityのMoveTowardsと同機能</remarks>
        /// <returns></returns>
        public static Vector3 GetLinearMovement(Vector3 current, Vector3 target, float delta)
        {
            var diffX = target.x - current.x;
            var diffY = target.y - current.y;
            var diffZ = target.z - current.z;

            var distSquare = (float)(diffX * (double)diffX + diffY * (double)diffY + diffZ * (double)diffZ);
            if (distSquare == 0.0 || delta >= 0.0 && distSquare <= delta * (double)delta)
            {
                return target;
            }

            var distance = (float)Math.Sqrt(distSquare);

            return new Vector3(
                current.x + diffX / distance * delta,
                current.y + diffY / distance * delta,
                current.z + diffZ / distance * delta
            );
        }
        
        /// <summary>
        /// Y軸を0にしたVector3を返す
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static Vector3 X0Z(this Vector3 v)
        {
            return new Vector3(v.x, 0, v.z);
        }

        public static float InverseLerp(this float a, float b, float value)
        {
            return (value - a) / (b - a);
        }

        public static bool IsInRange<T>(this T value, T min, T max) where T : struct, IComparable<T>
        {
            return value.CompareTo(min) >= 0 && value.CompareTo(max) <= 0;
        }

        public static bool ChangeMax<T>(ref this T value, T other) where T : struct, IComparable<T>
        {
            if (value.CompareTo(other) >= 0) return false;
            value = other;
            return true;
        }

        public static bool ChangeMin<T>(ref this T value, T other) where T : struct, IComparable<T>
        {
            if (value.CompareTo(other) <= 0) return false;
            value = other;
            return true;
        }
    }
}