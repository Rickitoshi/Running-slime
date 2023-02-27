using System;
using Signals;
using Zenject;

namespace Game.Managers
{
    public class Wallet: IInitializable,IDisposable
    {
        [Inject] private SignalBus _signalBus;
        [Inject] private SaveSystem _saveSystem;

        public int Coins => _currentCoins;
        
        private int _currentCoins;
        
        public void Initialize()
        {
            _currentCoins = _saveSystem.Data.Coins;

            _signalBus.Subscribe<CoinsAddSignal>(OnCoinsAdd);
            _signalBus.Subscribe<CoinsRemoveSignal>(OnCoinsRemove);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<CoinsAddSignal>(OnCoinsAdd);
            _signalBus.Unsubscribe<CoinsRemoveSignal>(OnCoinsRemove);
        }

        private void OnCoinsAdd(CoinsAddSignal signal)
        {
            _currentCoins += signal.Value;
            _saveSystem.Data.Coins = _currentCoins;
            _saveSystem.SaveData();
            _signalBus.Fire(new CoinsCountChangedSignal(_currentCoins));
        }
        
        private void OnCoinsRemove(CoinsRemoveSignal signal)
        {
            if (_currentCoins - signal.Value < 0) return;
            
            _currentCoins -= signal.Value;
            _saveSystem.Data.Coins = _currentCoins;
            _saveSystem.SaveData();
            _signalBus.Fire(new CoinsCountChangedSignal(_currentCoins));
        }

        public bool CanSpend(int value)
        {
            return _currentCoins - value >= 0;
        }
    }
}