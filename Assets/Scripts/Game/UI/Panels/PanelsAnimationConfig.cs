using UnityEngine;

namespace Game.UI.Panels
{
    [CreateAssetMenu(fileName = "PanelAnimationConfig", menuName = "Configs/Game/PanelAnimationConfig", order = 0)]
    public class PanelsAnimationConfig : ScriptableObject
    {
        [SerializeField] private float popUpStartScale;
        [SerializeField] private float popUpDurationScale;
        [SerializeField] private float panelDurationAlpha;

        public float PopUpStartScale => popUpStartScale;

        public float PopUpDurationScale => popUpDurationScale;

        public float PanelDurationAlpha => panelDurationAlpha;
    }
    
}