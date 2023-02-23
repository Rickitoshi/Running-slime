namespace Signals
{
    public class SoundSettingsChangedSignal
    {
        public readonly bool Value;

        public SoundSettingsChangedSignal(bool value)
        {
            Value = value;
        }
    }
}