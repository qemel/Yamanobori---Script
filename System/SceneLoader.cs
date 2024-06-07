using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rogue.Scripts.System
{
    public sealed class SceneLoader
    {
        private readonly TransitionScreen _transitionScreen;

        private bool _isLoading;

        public SceneLoader(TransitionScreen transitionScreen)
        {
            _transitionScreen = transitionScreen;
        }

        public async UniTask WaitAsync(CancellationToken cancellationToken)
        {
            if (!_isLoading) return;

            await UniTask.WaitUntil(() => !_isLoading, cancellationToken: cancellationToken);
        }

        public async UniTask LoadAsync(string sceneName, CancellationToken cancellationToken)
        {
            _isLoading = true;

            await _transitionScreen.ShowAsync(cancellationToken);

            await SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);

            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: cancellationToken);

            await Resources.UnloadUnusedAssets();

            await UniTask.Delay(TimeSpan.FromSeconds(0.1f), cancellationToken: cancellationToken);

            await _transitionScreen.HideAsync(cancellationToken);

            _isLoading = false;
        }
    }
}