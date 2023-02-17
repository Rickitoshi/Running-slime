using Game.Items;

namespace Game.Interfaces
{
    public interface IItemVisitor
    {
        public void CoinVisit(Coin coin,int cost);
    }
}