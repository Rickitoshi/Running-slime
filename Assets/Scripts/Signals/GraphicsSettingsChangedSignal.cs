using Game.UI;

namespace Signals
{
    public class GraphicsSettingsChangedSignal
    {
        public readonly GraphicsSettings Value;

        public GraphicsSettingsChangedSignal(GraphicsSettings value)
        {
            Value = value;
        }
    }
}