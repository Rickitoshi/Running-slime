using DG.Tweening;
using Game.Interfaces;
using UnityEngine;

namespace Game.Traps
{
    public class Saw: Obstacle
    {
        [SerializeField] private float rotateDuration = 2;
        [SerializeField] private float moveDuration = 3;
        [SerializeField] private Transform endPoint;
        
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            _transform.DORotate(Vector3.right * 360, rotateDuration, RotateMode.LocalAxisAdd).SetLoops(-1).SetEase(Ease.Linear);
            _transform.DOLocalMoveZ(endPoint.localPosition.z, moveDuration).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
        }
    }
}