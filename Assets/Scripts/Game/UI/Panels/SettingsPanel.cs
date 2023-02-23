using System;
using UnityEngine;
using UnityEngine.UI;

namespace Game.UI.Panels
{
    public class SettingsPanel: BasePanel
    {
        [Header("Graphics")]
        [SerializeField] private Toggle highToggle;
        [SerializeField] private Toggle lowToggle;
        [Header("Target FPS")]
        [SerializeField] private Toggle toggle60;
        [SerializeField] private Toggle toggle45;
        [SerializeField] private Toggle toggle30;
        [Header("Sound")]
        [SerializeField] private Toggle soundToggle;

        public void InitializeGraphicsToggles(GraphicsSettings graphicsSettings)
        {
            switch (graphicsSettings)
            {
                case GraphicsSettings.High:
                    highToggle.isOn = true;
                    break;
                case GraphicsSettings.Low:
                    lowToggle.isOn = true;
                    break;
            }
        }
        
        public void InitializeFPSToggles(TargetFPS targetFPS)
        {
            switch (targetFPS)
            {
                case TargetFPS.High:
                    toggle60.isOn = true;
                    break;
                case TargetFPS.Medium:
                    toggle45.isOn = true;
                    break;
                case TargetFPS.Low:
                    toggle30.isOn = true;
                    break;
            }
        }
        
        public void InitializeSoundToggle(bool isOn)
        {
            soundToggle.isOn = isOn;
        }
    }
}