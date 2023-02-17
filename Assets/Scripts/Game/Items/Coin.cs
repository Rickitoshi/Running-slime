using Game.Interfaces;
using UnityEngine;

namespace Game.Items
{
    public class Coin: BaseItem
    {
        [SerializeField] private int cost;

        protected override void OnVisit(IItemVisitor visitor)
        {
            visitor.CoinVisit(this, cost);
        }
    }
}