using Game.UI;

namespace Signals
{
    public class TargetFPSChangedSignal
    {
        public readonly TargetFPS Value;

        public TargetFPSChangedSignal(TargetFPS value)
        {
            Value = value;
        }
    }
}