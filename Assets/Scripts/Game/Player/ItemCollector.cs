using Game.Interfaces;
using Game.Items;
using UnityEngine;

namespace Game.Player
{
    public class ItemCollector : MonoBehaviour, IItemVisitor
    {
        public void CoinVisit(Coin coin, int cost)
        {
            coin.Deactivate();
            print(cost);
        }
    }
}