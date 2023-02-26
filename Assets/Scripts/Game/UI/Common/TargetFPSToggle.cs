using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class TargetFPSToggle : BaseToggle
    {
        [SerializeField] private TargetFPS targetFPS;

        protected override void OnValueChanged(bool value)
        {
            if (value)
            {
                _signalBus.Fire(new TargetFPSChangedSignal(targetFPS));
            }
        }
    }

    public enum TargetFPS
    {
        High=60,
        Low=30
    }
}