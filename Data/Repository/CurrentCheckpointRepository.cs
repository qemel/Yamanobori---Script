using Rogue.Scripts.View;

namespace Rogue.Scripts.Data.Repository
{
    public sealed class CurrentCheckpointRepository
    {
        private CheckpointView _currentCheckpoint;

        public bool TrySave(CheckpointView checkpoint)
        {
            if (_currentCheckpoint == null)
            {
                _currentCheckpoint = checkpoint;
                return true;
            }

            if (_currentCheckpoint.Id >= checkpoint.Id)
            {
                return false;
            }

            _currentCheckpoint = checkpoint;
            return true;
        }

        public CheckpointView Load()
        {
            return _currentCheckpoint;
        }
    }
}