using Game.Managers;

namespace Signals
{
    public class ChangeGameStateSignal
    {
        public readonly GameState State;

        public ChangeGameStateSignal(GameState state)
        {
            State = state;
        }
    }
}