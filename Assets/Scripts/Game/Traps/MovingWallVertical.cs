using DG.Tweening;
using UnityEngine;

namespace Game.Traps
{
    public class MovingWallVertical: Obstacle
    {
        [SerializeField] private float moveDuration = 3;
        [SerializeField] private Transform endPoint;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        public void Disarm()
        {
            _transform.DOLocalMoveY(endPoint.localPosition.y, moveDuration).SetEase(Ease.InOutBack);
        }
        
        public void Reset()
        {
            _transform.position = Vector3.zero;
        }
    }
}