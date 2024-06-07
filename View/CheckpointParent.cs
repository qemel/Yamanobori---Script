using System.Collections.Generic;
using Rogue.Scripts.Data.Repository;
using UnityEngine;

namespace Rogue.Scripts.View
{
    public sealed class CheckpointParent : MonoBehaviour
    {
        public IEnumerable<CheckpointView> Checkpoints => _checkpoints;
        private CheckpointView[] _checkpoints;

        public CheckpointView FirstCheckpoint => _firstCheckpoint;
        [SerializeField] private CheckpointView _firstCheckpoint;

        private void Awake()
        {
            _checkpoints = GetComponentsInChildren<CheckpointView>();
            
            if (!SettingRepository.IsHardcoreMode) return;
            
            foreach (var checkpoint in _checkpoints)
            {
                if (checkpoint.name != _firstCheckpoint.name)
                {
                    checkpoint.gameObject.SetActive(false);
                }
            }
        }
    }
}