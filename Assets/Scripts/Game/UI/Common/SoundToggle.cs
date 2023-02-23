using Signals;

namespace Game.UI
{
    public class SoundToggle : BaseToggle
    {
        protected override void OnValueChanged(bool value)
        {
            _signalBus.Fire(new SoundSettingsChangedSignal(value));
        }
    }
}