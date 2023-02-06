using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Debug : MonoBehaviour
{
    [SerializeField] private UniversalRenderPipelineAsset high;
    [SerializeField] private UniversalRenderPipelineAsset low;
    
    public void Medium()
    {
        GraphicsSettings.renderPipelineAsset = high;
        QualitySettings.renderPipeline = high;
    }
    
    public void Low()
    {
        GraphicsSettings.renderPipelineAsset = low;
        QualitySettings.renderPipeline = low;
    }
}
