using System;
using Signals;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class CoinCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;

        [Inject] private SignalBus _signalBus;

        private void Awake()
        {
            _signalBus.Subscribe<CoinsCountChangedSignal>(OnCoinsCountChanged);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<CoinsCountChangedSignal>(OnCoinsCountChanged);
        }

        private void OnCoinsCountChanged(CoinsCountChangedSignal signal)
        {
            label.text = signal.Value.ToString();
        }

        public void Initialize(int value)
        {
            label.text = value.ToString();
        }
    }
}