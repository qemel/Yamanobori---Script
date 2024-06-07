using System;
using R3;
using Rogue.Scripts.Data;
using Rogue.Scripts.Data.Repository;
using Rogue.Scripts.View;
using VContainer.Unity;

namespace Rogue.Scripts.System.EntryPoint
{
    public sealed class CheckPointSystem : IStartable, IDisposable
    {
        private readonly CheckpointParent _checkpointParent;
        private readonly CurrentCheckpointRepository _currentCheckpointRepository;

        private readonly CompositeDisposable _disposable = new();

        public CheckPointSystem(CurrentCheckpointRepository currentCheckpointRepository,
            CheckpointParent checkpointParent, CheckPointSE checkPointSe)
        {
            _currentCheckpointRepository = currentCheckpointRepository;
            _checkpointParent = checkpointParent;
        }

        public void Start()
        {
            _currentCheckpointRepository.TrySave(_checkpointParent.FirstCheckpoint);

            foreach (var checkpoint in _checkpointParent.Checkpoints)
            {
                checkpoint.OnPlayerTouched.Subscribe(_ =>
                    {
                        var isSaved = _currentCheckpointRepository.TrySave(checkpoint);
                        if (isSaved)
                        {
                            checkpoint.OnCheckPointSaved();
                        }
                    })
                    .AddTo(_disposable);
            }
        }

        public void Dispose()
        {
            _disposable.Dispose();
        }
    }
}