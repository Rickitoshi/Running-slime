namespace Signals
{
    public class CoinsCountChangedSignal
    {
        public readonly int Value;

        public CoinsCountChangedSignal(int value)
        {
            Value = value;
        }
    }
}