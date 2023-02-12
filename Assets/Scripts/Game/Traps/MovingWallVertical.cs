using System;
using DG.Tweening;
using UnityEngine;

namespace Game.Traps
{
    public class MovingWallVertical: Obstacle
    {
        [SerializeField] private float moveDuration = 3;
        [SerializeField] private Transform endPoint;

        private Transform _transform;
        private Vector3 _startPosition;

        private void Awake()
        {
            _transform = transform;
            _startPosition = _transform.localPosition;
        }

        public void Disarm()
        {
            _transform.DOLocalMoveY(endPoint.localPosition.y, moveDuration).SetEase(Ease.InOutBack);
        }
        
        public void Reset()
        {
            _transform.localPosition = _startPosition;
        }
    }
}