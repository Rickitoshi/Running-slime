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
        [SerializeField] private BasePanel marketPanel;
        [SerializeField] private BasePanel losePanel;
        [SerializeField] private SettingsPanel settingsPanel;
        [SerializeField] private BasePanel pausePanel;
        [SerializeField] private BasePanel exitPanel;
        
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
            marketPanel.Initialize(_panelsConfig);
            
            settingsPanel.InitializeGraphicsToggles(_saveSystem.Data.GraphicsSettings);
            settingsPanel.InitializeFPSToggles(_saveSystem.Data.TargetFPS);
            settingsPanel.InitializeSoundToggle(_saveSystem.Data.IsSoundOn);
            settingsPanel.Initialize(_panelsConfig);
            
            pausePanel.Initialize(_panelsConfig);
            exitPanel.Initialize(_panelsConfig);

            ChangePanel(menuPanel);
        }

        private void OnDestroy()
        {
           Unsubscribe();
        }

        private void Subscribe()
        {
            _signalBus.Subscribe<ChangeUIStateSignal>(OnChangeUIState);
            _signalBus.Subscribe<OnPlayerDieSignal>(OnPlayerDie);
        }

        private void Unsubscribe()
        {
            _signalBus.Unsubscribe<ChangeUIStateSignal>(OnChangeUIState);
            _signalBus.Unsubscribe<OnPlayerDieSignal>(OnPlayerDie);
        }
        
        private void OnChangeUIState(ChangeUIStateSignal signal)
        {
            switch (signal.State)
            {
                case UIState.Game:
                    ChangePanel(gamePanel);
                    break;
                case UIState.Menu:
                    ChangePanel(menuPanel);
                    gamePanel.ResetScoreValue();
                    break;
                case UIState.Pause:
                    ChangePanel(pausePanel);
                    break;
                case UIState.Settings:
                    ChangePanel(settingsPanel);
                    break;
                case UIState.Market:
                    ChangePanel(marketPanel);
                    break;
                case UIState.ExitConfirmation:
                    ChangePanel(exitPanel);
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
    }
    
    public enum UIState
    {
        None,
        Game,
        Menu,
        Pause,
        Settings,
        Market,
        ExitConfirmation,
    }
}