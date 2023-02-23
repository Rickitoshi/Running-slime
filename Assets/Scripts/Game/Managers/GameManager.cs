using System;
using Game.UI;
using Signals;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Zenject;

namespace Game.Managers
{
    public class GameManager: MonoBehaviour
    {
        [SerializeField] private UniversalRenderPipelineAsset highGraphicsPreset;
        [SerializeField] private UniversalRenderPipelineAsset lowGraphicsPreset;
        
        [Inject] private SignalBus _signalBus;
        [Inject] private SaveSystem _saveSystem;
        

        public void Start()
        {
            InitializeGameSettings();
            
            Subscribe();
        }

        public void OnDestroy()
        {
            _saveSystem.SaveData();
           Unsubscribe();
        }
        
        private void Subscribe()
        {
            _signalBus.Subscribe<GraphicsSettingsChangedSignal>(OnGraphicsChanged);
            _signalBus.Subscribe<TargetFPSChangedSignal>(OnTargetFPSChanged);
            _signalBus.Subscribe<SoundSettingsChangedSignal>(OnSoundSettingsChanged);
        }

        private void Unsubscribe()
        {
            _signalBus.Unsubscribe<GraphicsSettingsChangedSignal>(OnGraphicsChanged);
            _signalBus.Unsubscribe<TargetFPSChangedSignal>(OnTargetFPSChanged);
            _signalBus.Unsubscribe<SoundSettingsChangedSignal>(OnSoundSettingsChanged);
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
        }

        private void OnTargetFPSChanged(TargetFPSChangedSignal signal)
        {
            Application.targetFrameRate = (int)signal.Value;
            _saveSystem.Data.TargetFPS = signal.Value;
        }

        private void OnSoundSettingsChanged(SoundSettingsChangedSignal signal)
        {
            _saveSystem.Data.IsSoundOn = signal.Value;
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
    }
    
    public enum GameState
    {
        None,
        Game,
        Menu,
        Pause,
        Settings,
        Market,
        Unpause,
        BackToMenu
    }
}