using Zenject;
using UnityEngine;

namespace Game.Player
{
    public class InputHandler: ITickable
    {
        private const float SWIPE_DEAD_ZONE = 0.003f;
        private const int TOUCH_FRAME_DEAD_ZONE = 3;
    
        public bool IsLeftSwipe { get; private set; }
        public bool IsRightSwipe { get; private set; }
        public bool IsTouch { get; private set; }

        private Vector3 _prevPos = Vector3.zero;
        private Vector2 _normalizeDelta =  Vector2.zero;
        private bool _isHold;
        private bool _isTouched;
        private bool _isSwiped;
        private int _holdFrameCount;

        public void Tick()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isHold = true;
                _prevPos = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isHold = false;
                _holdFrameCount = 0;
                _isTouched = false;
                _isSwiped = false;
                _prevPos = Vector3.zero;
            }
            
            if (_isHold)
            {
                CalculateDelta();

                _holdFrameCount++;

                if (_isSwiped)
                {
                    IsRightSwipe = false;
                    IsLeftSwipe = false;
                }

                if (_isTouched)
                {
                    IsTouch = false;
                }

                if (_normalizeDelta != Vector2.zero)
                {
                    if (Mathf.Abs(_normalizeDelta.x) > SWIPE_DEAD_ZONE && !_isSwiped && !_isTouched)
                    {
                        _isSwiped = true;

                        IsRightSwipe = _normalizeDelta.x > 0;
                        IsLeftSwipe = _normalizeDelta.x < 0;
                    }
                }
                else
                {
                    if (_holdFrameCount >= TOUCH_FRAME_DEAD_ZONE && !_isTouched && !_isSwiped)
                    {
                        IsTouch = true;
                        _isTouched = true;
                    }
                }
                
            }
        }

        private void CalculateDelta()
        {
            Vector3 delta = Input.mousePosition - _prevPos;

            _normalizeDelta.x = delta.x / Screen.width;
            _normalizeDelta.y = delta.y / Screen.height;

            _prevPos = Input.mousePosition;
        }
    }
}
