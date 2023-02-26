using Cinemachine;
using Game.UI;
using Signals;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace Game.Managers
{
    public class GameManager: MonoBehaviour
    {
        [Header("Graphics preset")]
        [SerializeField] private UniversalRenderPipelineAsset highGraphicsPreset;
        [SerializeField] private UniversalRenderPipelineAsset lowGraphicsPreset;
        [Header("Cameras")]
        [SerializeField] private CinemachineVirtualCamera gameCamera;
        [SerializeField] private CinemachineVirtualCamera menuCamera;
        [SerializeField] private CinemachineVirtualCamera marketCamera;
        
        [Inject] private SignalBus _signalBus;
        [Inject] private SaveSystem _saveSystem;

        private CinemachineVirtualCamera _currentCamera;
            
        public void Start()
        {
            InitializeGameSettings();
            InitializeCameras(menuCamera);
            
            Subscribe();
        }

        public void OnDestroy()
        {
            Unsubscribe();
        }
        
        private void Subscribe()
        {
            _signalBus.Subscribe<GraphicsSettingsChangedSignal>(OnGraphicsChanged);
            _signalBus.Subscribe<TargetFPSChangedSignal>(OnTargetFPSChanged);
            _signalBus.Subscribe<SoundSettingsChangedSignal>(OnSoundSettingsChanged);
            _signalBus.Subscribe<ChangeGameStateSignal>(OnGameStateChange);
        }

        private void Unsubscribe()
        {
            _signalBus.Unsubscribe<GraphicsSettingsChangedSignal>(OnGraphicsChanged);
            _signalBus.Unsubscribe<TargetFPSChangedSignal>(OnTargetFPSChanged);
            _signalBus.Unsubscribe<SoundSettingsChangedSignal>(OnSoundSettingsChanged);
            _signalBus.Unsubscribe<ChangeGameStateSignal>(OnGameStateChange);
        }

        private void OnGameStateChange(ChangeGameStateSignal signal)
        {
            switch (signal.State)
            {
                case GameState.Game:
                    ChangeCamera(gameCamera);
                    break;
                case GameState.Menu:
                    ChangeCamera(menuCamera);
                    Time.timeScale = 1;
                    break;
                case GameState.Pause:
                    Time.timeScale = 0;
                    break;
                case GameState.Market:
                    ChangeCamera(marketCamera);
                    break;
                case GameState.Unpause:
                    Time.timeScale = 1;
                    break;
                case GameState.Exit:
                    Application.Quit();
                    break;
            }
        }
        
        private void OnGraphicsChanged(GraphicsSettingsChangedSignal signal)
        {
            switch (signal.Value)
            {
                case GraphicsSettings.High:
                    QualitySettings.renderPipeline = highGraphicsPreset;
                    break;
                case GraphicsSettings.Low:
                    QualitySettings.renderPipeline = lowGraphicsPreset;
                    break;
            }

            _saveSystem.Data.GraphicsSettings = signal.Value;
            _saveSystem.SaveData();
        }

        private void OnTargetFPSChanged(TargetFPSChangedSignal signal)
        {
            Application.targetFrameRate = (int)signal.Value;
            _saveSystem.Data.TargetFPS = signal.Value;
            _saveSystem.SaveData();
        }

        private void OnSoundSettingsChanged(SoundSettingsChangedSignal signal)
        {
            _saveSystem.Data.IsSoundOn = signal.Value;
            _saveSystem.SaveData();
            //TODO Добавить вкл/выкл звука
        }

        private void InitializeGameSettings()
        {
            switch (_saveSystem.Data.GraphicsSettings)
            {
                case GraphicsSettings.High:
                    QualitySettings.renderPipeline = highGraphicsPreset;
                    break;
                case GraphicsSettings.Low:
                    QualitySettings.renderPipeline = lowGraphicsPreset;
                    break;
            }
            
            Application.targetFrameRate = (int)_saveSystem.Data.TargetFPS;
            
            //TODO Добавить вкл/выкл звука
        }

        private void InitializeCameras(CinemachineVirtualCamera startCamera)
        {
            gameCamera.Priority = 0;
            menuCamera.Priority = 0;
            marketCamera.Priority = 0;

            _currentCamera = startCamera;
            _currentCamera.Priority = 1;
        }

        private void ChangeCamera(CinemachineVirtualCamera newCamera)
        {
            _currentCamera.Priority = 0;
            _currentCamera = newCamera;
            _currentCamera.Priority = 1;
        }
    }
    
    public enum GameState
    {
        None,
        Game,
        Menu,
        Pause,
        Market,
        Unpause,
        Exit
    }
}