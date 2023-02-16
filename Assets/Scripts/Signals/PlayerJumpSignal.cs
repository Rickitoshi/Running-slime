namespace Signals
{
    public class PlayerJumpSignal
    {
        public readonly float PositionZ;

        public PlayerJumpSignal(float positionZ)
        {
            PositionZ = positionZ;
        }
    }
}