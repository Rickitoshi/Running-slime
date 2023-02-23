using Signals;
using UnityEngine;

namespace Game.UI
{
    public class GraphicsToggle : BaseToggle
    {
        [SerializeField] private GraphicsSettings graphicsSettings;

        protected override void OnValueChanged(bool value)
        {
            if (value)
            {
                _signalBus.Fire(new GraphicsSettingsChangedSignal(graphicsSettings));
            }
        }
    }

    public enum GraphicsSettings
    {
        High,
        Low
    }
}