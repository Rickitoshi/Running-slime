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
        [SerializeField] private BasePanel settingsPanel;
        [SerializeField] private BasePanel pausePanel;
        
        [Inject] private SignalBus _signalBus;
        [Inject] private PanelsAnimationConfig _panelsConfig;
        
        private BasePanel _currentPanel;
        private BasePanel _pastPanel;

        private void Awake()
        {
            _signalBus.Subscribe<ChangePanelUISignal>(OnChangePanel);
        }

        private void Start()
        {
            menuPanel.Initialize(_panelsConfig);
            gamePanel.Initialize(_panelsConfig, 100);
            losePanel.Initialize(_panelsConfig);
            settingsPanel.Initialize(_panelsConfig);
            pausePanel.Initialize(_panelsConfig);

            ChangePanel(menuPanel);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<ChangePanelUISignal>(OnChangePanel);
        }

        private void OnChangePanel(ChangePanelUISignal signal)
        {
            switch (signal.Type)
            {
                case PanelType.Game:
                    ChangePanel(gamePanel);
                    gamePanel.ResetScoreValue();
                    break;
                case PanelType.Menu:
                    ChangePanel(menuPanel);
                    break;
                case PanelType.Pause:
                    ChangePanel(pausePanel);
                    break;
                case PanelType.Lose:
                    ChangePanel(losePanel);
                    break;
                case PanelType.Settings:
                    ChangePanel(settingsPanel);
                    break;
                case PanelType.Market:
                    break;
                case PanelType.Back:
                    ChangePanel(_pastPanel);
                    break;
            }
        }
        
        private void ChangePanel(BasePanel newPanel)
        {
            if (_currentPanel)
            {
                _currentPanel.Deactivate();
                _pastPanel = _currentPanel;
            }

            _currentPanel = newPanel;
            _currentPanel.Activate();
        }
    }

    public enum PanelType
    {
        Game,
        Menu,
        Pause,
        Lose,
        Settings,
        Market,
        Back
    }
}