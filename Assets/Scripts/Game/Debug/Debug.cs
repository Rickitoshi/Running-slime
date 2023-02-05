using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Debug : MonoBehaviour
{
    public void OnShadows()
    {
        QualitySettings.shadows = ShadowQuality.HardOnly;
    }
    
    public void OffShadows()
    {
        QualitySettings.shadows = ShadowQuality.Disable;
    }
}
