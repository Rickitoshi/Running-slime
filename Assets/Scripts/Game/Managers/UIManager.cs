using System;
using Game.UI.Panels;
using Signals;
using UnityEngine;
using Zenject;

namespace Game.Managers
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private BasePanel menuPanel;
        [SerializeField] private GamePanel gamePanel;
        [SerializeField] private BasePanel losePanel;
        [SerializeField] private SettingsPanel settingsPanel;
        [SerializeField] private BasePanel pausePanel;
        
        [Inject] private SignalBus _signalBus;
        [Inject] private SaveSystem _saveSystem;
        [Inject] private PanelsAnimationConfig _panelsConfig;
        
        private BasePanel _currentPanel;

        private void Awake()
        {
            Subscribe();
        }

        private void Start()
        {
            menuPanel.Initialize(_panelsConfig);
            gamePanel.Initialize(_panelsConfig);
            losePanel.Initialize(_panelsConfig);
            
            settingsPanel.InitializeGraphicsToggles(_saveSystem.Data.GraphicsSettings);
            settingsPanel.InitializeFPSToggles(_saveSystem.Data.TargetFPS);
            settingsPanel.InitializeSoundToggle(_saveSystem.Data.IsSoundOn);
            settingsPanel.Initialize(_panelsConfig);
            
            pausePanel.Initialize(_panelsConfig);

            ChangePanel(menuPanel);
        }

        private void OnDestroy()
        {
           Unsubscribe();
        }

        private void OnChangeGameState(ChangeGameStateSignal signal)
        {
            switch (signal.State)
            {
                case GameState.Game:
                    ChangePanel(gamePanel);
                    gamePanel.ResetScoreValue();
                    gamePanel.InitializeCoinCounter(111);
                    break;
                case GameState.Menu:
                    ChangePanel(menuPanel);
                    break;
                case GameState.Pause:
                    ChangePanel(pausePanel);
                    break;
                case GameState.Settings:
                    ChangePanel(settingsPanel);
                    break;
                case GameState.Market:
                    break;
                case GameState.Unpause:
                    ChangePanel(gamePanel);
                    break;
                case GameState.BackToMenu:
                    ChangePanel(menuPanel);
                    break;
            }
        }
        
        private void ChangePanel(BasePanel newPanel)
        {
            if (_currentPanel)
            {
                _currentPanel.Deactivate();
            }

            _currentPanel = newPanel;
            _currentPanel.Activate();
        }

        private void OnPlayerDie()
        {
            ChangePanel(losePanel);
        }

        private void Subscribe()
        {
            _signalBus.Subscribe<ChangeGameStateSignal>(OnChangeGameState);
            _signalBus.Subscribe<OnPlayerDieSignal>(OnPlayerDie);
        }

        private void Unsubscribe()
        {
            _signalBus.Unsubscribe<ChangeGameStateSignal>(OnChangeGameState);
            _signalBus.Unsubscribe<OnPlayerDieSignal>(OnPlayerDie);
        }
    }
}