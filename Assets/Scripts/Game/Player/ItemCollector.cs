using Game.Interfaces;
using Game.Items;
using Signals;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class ItemCollector : MonoBehaviour, IItemVisitor
    {
        [Inject] private SignalBus _signalBus;
        
        public void CoinVisit(Coin coin, int cost)
        {
            coin.Deactivate();
            _signalBus.Fire(new CoinsAddSignal(cost));
        }
    }
}