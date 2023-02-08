using DG.Tweening;
using UnityEngine;

namespace Game.Traps
{
    public class MovingWallHorizontal: Obstacle
    {
        [SerializeField] private float moveDuration = 3;
        [SerializeField] private Transform endPoint;

        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            _transform.DOLocalMoveZ(endPoint.localPosition.z, moveDuration).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.InOutQuad);
        }
    }
}