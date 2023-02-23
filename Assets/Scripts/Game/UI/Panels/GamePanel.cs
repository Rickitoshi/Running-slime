using UnityEngine;

namespace Game.UI.Panels
{
    public class GamePanel: BasePanel
    {
        [SerializeField] private CoinCounter coinCounter;
        [SerializeField] private ScoreCounter scoreCounter;

        public void InitializeCoinCounter(int coinsValue)
        {
            coinCounter.Initialize(coinsValue);
        }

        public void ResetScoreValue()
        {
            scoreCounter.ResetValue();
            
        }
    }
}