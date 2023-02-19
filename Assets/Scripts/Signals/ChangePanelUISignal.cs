using Game.Managers;

namespace Signals
{
    public class ChangePanelUISignal
    {
        public readonly PanelType Type;

        public ChangePanelUISignal(PanelType type)
        {
            Type = type;
        }
    }
}