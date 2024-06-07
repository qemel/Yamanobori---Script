using UnityEngine;

namespace Rogue.Scripts.Data
{
    [CreateAssetMenu(fileName = "NewCheckPointSE", menuName = "CheckPointSE")]
    public sealed class CheckPointSE : ScriptableObject
    {
        public AudioClip CheckPointSound => _checkPointSound;
        [SerializeField] private AudioClip _checkPointSound;
    }
}