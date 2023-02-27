using DG.Tweening;
using UnityEngine;

namespace Game.UI.Panels
{
    [RequireComponent(typeof(CanvasGroup))]
    public class BasePanel : MonoBehaviour
    {
        private CanvasGroup _canvasGroup;
        private float _alphaDuration;

        public void Initialize(PanelsAnimationConfig config)
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            
            _alphaDuration = config.PanelDurationAlpha;

            gameObject.SetActive(false);
            _canvasGroup.alpha = 0;
        }

        public void Activate()
        {
            gameObject.SetActive(true);
            _canvasGroup.DOFade(1, _alphaDuration).SetUpdate(true).OnComplete(() => { SetInteractable(true); });
        }

        public void Deactivate()
        {
            SetInteractable(false);
            _canvasGroup.DOFade(0, _alphaDuration).SetUpdate(true).OnComplete(() => { gameObject.SetActive(false); });
        }
        
        private void SetInteractable(bool value)
        {
            _canvasGroup.interactable = value;
        }
    }
}