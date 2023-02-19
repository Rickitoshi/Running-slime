using Signals;
using TMPro;
using UnityEngine;
using Zenject;

namespace Game.UI
{
    public class ScoreCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI label;
        
        [Inject] private SignalBus _signalBus;
        private int _currentScore;

        private void Awake()
        {
            _signalBus.Subscribe<ScoreChangedSignal>(OnScoreChanged);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<ScoreChangedSignal>(OnScoreChanged);
        }

        private void OnScoreChanged()
        {
            label.text = _currentScore++.ToString();
        }

        public void ResetValue()
        {
            _currentScore = 0;
            label.text = _currentScore.ToString();
        }
    }
}