using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public abstract class BaseToggle : MonoBehaviour
    {
        [Inject] protected SignalBus _signalBus;
        private Toggle _toggle;
        
        private void Awake()
        {
            _toggle = GetComponent<Toggle>();
        }

        private void Start()
        {
            _toggle.onValueChanged.AddListener(OnValueChanged);
        }

        private void OnDestroy()
        {
            _toggle.onValueChanged.RemoveListener(OnValueChanged);
        }

        protected abstract void OnValueChanged(bool value);
    }
}