using DG.Tweening;
using UnityEngine;

namespace Game.Traps
{
    public class HorizontalSaw : Obstacle
    {
        [SerializeField] private float maxRotateDuration = 3;
        [SerializeField] private float minRotateDuration = 2;

        private Transform _transform;
        private float _rotateDuration;
        private int _angle;

        private void Awake()
        {
            _transform = transform;
        }

        private void OnEnable()
        {
            transform.DORotate(Vector3.up * _angle, _rotateDuration, RotateMode.LocalAxisAdd).SetLoops(-1).SetEase(Ease.Linear);
        }

        private void OnDisable()
        {
            DOTween.Kill(transform);
            _transform.localRotation = Quaternion.identity;
            _rotateDuration = Random.Range(minRotateDuration, maxRotateDuration);
            _angle = Random.Range(0, 2) != 0 ? -360 : 360;
        }
    }
}