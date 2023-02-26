using Game.Managers;

namespace Signals
{
    public class ChangeUIStateSignal
    {
        public readonly UIState State;

        public ChangeUIStateSignal(UIState state)
        {
            State = state;
        }
    }
}