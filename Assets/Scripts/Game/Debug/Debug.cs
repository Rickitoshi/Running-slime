using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Debug : MonoBehaviour
{
    [SerializeField] private UniversalRenderPipelineAsset high;
    [SerializeField] private UniversalRenderPipelineAsset low;
    
    public void Medium()
    {
        QualitySettings.renderPipeline = high;
    }
    
    public void Low()
    {
        QualitySettings.renderPipeline = low;
    }

    public void Exit()
    {
        Application.Quit();
    }
}
