using R3;
using UnityEngine;
using UnityEngine.UI;

namespace Rogue.Scripts.View.UI
{
    [RequireComponent(typeof(Button))]
    public class ButtonBaseView : MonoBehaviour
    {
        private Button _button;

        public Observable<Unit> OnPressed => _onPressed;
        private readonly Subject<Unit> _onPressed = new();

        private void Awake()
        {
            _button = GetComponent<Button>();

            PostAwake();
        }

        protected virtual void PostAwake()
        {
        }

        private void Start()
        {
            _button.OnClickAsObservable().Subscribe(_ => _onPressed.OnNext(Unit.Default)).AddTo(this);

            PostStart();
        }

        protected virtual void PostStart()
        {
        }
    }
}