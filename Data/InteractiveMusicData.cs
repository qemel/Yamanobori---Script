using UnityEngine;

namespace Rogue.Scripts.Data
{
    [CreateAssetMenu(fileName = "NewInteractiveMusicData", menuName = "InteractiveMusicData")]
    public sealed class InteractiveMusicData : ScriptableObject
    {
        public float P2PlayerPositionY => _p2PlayerPositionY;
        [SerializeField] private float _p2PlayerPositionY;
        
        public float P3PlayerPositionY => _p3PlayerPositionY;
        [SerializeField] private float _p3PlayerPositionY;
        
        public float P4PlayerPositionY => _p4PlayerPositionY;
        [SerializeField] private float _p4PlayerPositionY;
    }
}