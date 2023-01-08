using Zenject;
using UnityEngine;

namespace Game.Player
{
    public class InputHandler: ITickable
    {
        private const float DEAD_ZONE = 0.004f;
    
        public bool IsLeftSwipe { get; private set; }
        public bool IsRightSwipe { get; private set; }
        public bool IsTouch => _isTouched;

        private Vector3 _prevPos = Vector3.zero;
        private Vector2 _normalizeDelta =  Vector2.zero;
        private bool _isHold;
        private bool _isTouched;
        private bool _isSwiped;

        public void Tick()
        {
            if (_isTouched)
            {
                _isTouched = false;
            }

            if (Input.GetMouseButtonDown(0))
            {
                _isHold = true;
                _isTouched = true;
                _prevPos = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(0))
            {
                _isHold = false;
                _prevPos = Vector3.zero;
            }
            
            if (_isHold)
            {
                CalculateDelta();

                if (_normalizeDelta != Vector2.zero)
                {
                    if (Mathf.Abs(_normalizeDelta.x) > DEAD_ZONE && !_isSwiped)
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
            else
            {
                _isSwiped = false;
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
