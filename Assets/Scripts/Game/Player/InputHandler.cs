using Zenject;
using UnityEngine;

namespace Game.Player
{
    public class InputHandler: ITickable
    {
        private const float SWIPE_DEAD_ZONE = 0.01f;
        private const float CHECK_TOUCH_TIME = 0.06f;
    
        public bool IsLeftSwipe { get; private set; }
        public bool IsRightSwipe { get; private set; }
        public bool IsTouch { get; private set; }

        private Vector3 _startPos = Vector3.zero;
        private Vector2 _normalizeDelta =  Vector2.zero;
        private bool _isHold;
        private bool _isTouched;
        private bool _isSwiped;
        private float _holdTime;

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isHold = true;
                _startPos = Input.mousePosition;
            }
            
            if (_isHold)
            {
                _holdTime += Time.deltaTime;

                CalculateDelta();
                
                if (!_isSwiped)
                {
                    if (Mathf.Abs(_normalizeDelta.x) > SWIPE_DEAD_ZONE)
                    {
                        _isSwiped = true;

                        IsRightSwipe = _normalizeDelta.x > 0;
                        IsLeftSwipe = _normalizeDelta.x < 0;
                    }
                }
                else
                {
                    IsRightSwipe = false;
                    IsLeftSwipe = false;
                }
            }

            if (IsTouch) IsTouch = false;
            
            if (Input.GetMouseButtonUp(0))
            {
                _holdTime = 0;
                _isHold = false;
                _startPos = Vector3.zero;

                if (_holdTime <= CHECK_TOUCH_TIME && !_isSwiped)
                {
                    IsTouch = true;
                }
                
                _isSwiped = false;
                IsRightSwipe = false;
                IsLeftSwipe = false;
            }
        }

        private void CalculateDelta()
        {
            Vector3 delta = Input.mousePosition - _startPos;

            _normalizeDelta.x = delta.x / Screen.width;
            _normalizeDelta.y = delta.y / Screen.height;
        }
    }
}
