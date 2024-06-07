using UnityEngine;

namespace Rogue.Scripts.Data
{
    public static class Sound
    {
        private static readonly float DefaultBgmVolume = PlayerPrefs.GetFloat("BGMVolume", 0.3f);
        private static readonly float DefaultSeVolume = PlayerPrefs.GetFloat("SEVolume", 0.5f);

        static Sound()
        {
            Setting = new SoundSetting(DefaultBgmVolume, DefaultSeVolume);
        }

        public static SoundSetting Setting { get; }
    }
}