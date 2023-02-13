using System;
using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Traps
{
    public class Trolley: Obstacle
    {
        [SerializeField] private float maxMoveDuration = 4;
        [SerializeField] private float minMoveDuration = 2;
        [SerializeField] private Transform endPoint;

        private Transform _transform;
        private float _moveDuration;
        private Vector3 _startPoint;

        private void Awake()
        {
            _transform = transform;
            _startPoint = _transform.localPosition;
        }

        private void OnEnable()
        {
            _transform.DOLocalMoveX(endPoint.localPosition.x, _moveDuration).SetLoops(-1,LoopType.Yoyo).SetEase(Ease.Linear);
        }

        private void OnDisable()
        {
            DOTween.Kill(transform);
            _transform.localPosition = _startPoint;
            _moveDuration = Random.Range(minMoveDuration, maxMoveDuration);
        }
    }
}