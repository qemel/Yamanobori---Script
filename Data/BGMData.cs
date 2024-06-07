using UnityEngine;

namespace Rogue.Scripts.Data
{
    [CreateAssetMenu(fileName = "BGMData", menuName = "BGMData")]
    public sealed class BGMData : ScriptableObject
    {
        public AudioClip MainP1 => _mainP1;
        [SerializeField] private AudioClip _mainP1;

        public AudioClip MainP2 => _mainP2;
        [SerializeField] private AudioClip _mainP2;

        public AudioClip MainP3 => _mainP3;
        [SerializeField] private AudioClip _mainP3;

        public AudioClip MainP4 => _mainP4;
        [SerializeField] private AudioClip _mainP4;
    }
}