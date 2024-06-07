using UnityEngine;

namespace Rogue.Scripts.View
{
    public sealed class TitleLightView : MonoBehaviour
    {
        [SerializeField] private float _speed;

        private void Update()
        {
            var qua = Quaternion.AngleAxis(Time.deltaTime * _speed, Vector3.up);
            transform.rotation = qua * transform.rotation;
        }
    }
}