using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Player
{
    public class InputHandler : MonoBehaviour, IBeginDragHandler, IDragHandler, IPointerClickHandler,IPointerUpHandler
    {
        private const float SWIPE_DEAD_ZONE = 5f;

        public event Action<float> OnHorizontalSwap;
        public event Action<float> OnVerticalSwap;
        public event Action OnClick;

        private Vector2 _normalizeDelta;
        private bool _isSwiped;

        public void OnBeginDrag(PointerEventData eventData)
        {
            Vector2 delta = eventData.delta;

            if ((Mathf.Abs(delta.x + delta.y) < SWIPE_DEAD_ZONE)) return;
            
            _isSwiped = true;

            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                OnHorizontalSwap?.Invoke(delta.x);
            }
            else
            {
                OnVerticalSwap?.Invoke(delta.y);
            }
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            
        }

        public void OnPointerClick(PointerEventData eventData)
        {

        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_isSwiped)
            {
                _isSwiped = false;
            }
            else
            {
                OnClick?.Invoke();
            }
        }
    }
}