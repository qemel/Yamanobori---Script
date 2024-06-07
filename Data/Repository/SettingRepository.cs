using System;
using UnityEngine;

namespace Rogue.Scripts.Data.Repository
{
    public static class SettingRepository
    {
        /// <summary>
        /// 実際の感度　
        /// </summary>
        public static float ControllerSensitivity => ControllerSensitivityBase * SensitivityMultiplier;

        /// <summary>
        /// 感度倍率
        /// </summary>
        private const float SensitivityMultiplier = 28.0f;

        public static float ControllerSensitivityBase
        {
            get => _controllerSensitivityBase;
            set
            {
                if (!IsValid(value)) throw new ArgumentException("0.0f以上1.0f以下の値を設定してください");

                _controllerSensitivityBase = value;
                PlayerPrefs.SetFloat("ControllerSensitivityBase", value);
            }
        }

        private static float _controllerSensitivityBase = PlayerPrefs.GetFloat("ControllerSensitivityBase", 0.5f);

        /// <summary>
        /// 実際のFOV
        /// </summary>
        /// <returns></returns>
        public static float Fov => Mathf.Lerp(60.0f, 100.0f, FovBase);

        public static float FovBase
        {
            get => _fovBase;
            set
            {
                if (!IsValid(value)) throw new ArgumentException("0.0f以上1.0f以下の値を設定してください");

                _fovBase = value;
                PlayerPrefs.SetFloat("FovBase", value);
            }
        }

        private static float _fovBase = PlayerPrefs.GetFloat("FovBase", 0.5f);

        public static bool IsHardcoreMode { get; set; }
        
        /// <summary>
        /// 設定の値は0.0f以上1.0f以下
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        private static bool IsValid(float val)
        {
            return val is >= 0.0f and <= 1.0f;
        }
    }
}