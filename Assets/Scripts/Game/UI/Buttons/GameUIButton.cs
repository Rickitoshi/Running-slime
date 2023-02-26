using System;
using Game.Managers;
using Signals;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Game.UI
{
    public class GameUIButton : MonoBehaviour
    {
        [SerializeField] private UIState setUIState;
        [SerializeField] private GameState setGameState;
        
        [Inject] private SignalBus _signalBus;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(OnClick);
        }

        private void OnDestroy()
        {
            _button.onClick.RemoveListener(OnClick);
        }

        private void OnClick()
        {
            if (setGameState != GameState.None)
            {
                _signalBus.Fire(new ChangeGameStateSignal(setGameState));
            }

            if (setUIState != UIState.None)
            {
                _signalBus.Fire(new ChangeUIStateSignal(setUIState));
            }
        }
    }
}