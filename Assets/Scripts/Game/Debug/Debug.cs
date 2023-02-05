using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using ShadowQuality = UnityEngine.ShadowQuality;

public class Debug : MonoBehaviour
{
    [SerializeField] private UniversalRenderPipelineAsset noShadows;
    [SerializeField] private UniversalRenderPipelineAsset shadows;
    
    public void OnShadows()
    {
        GraphicsSettings.renderPipelineAsset = shadows;
    }
    
    public void OffShadows()
    {
        GraphicsSettings.renderPipelineAsset = noShadows;
    }
}
