using DG.Tweening;
using UnityEngine;

namespace Game.Traps
{
    public class Bullet: Obstacle
    {
        private const float MAX_DISTANCE = 20f;
        
        [SerializeField] private float moveDuration;
        [SerializeField] private LayerMask groundLayerMask;

        private Vector3 _startPosition;
        private Transform _transform;

        private void Awake()
        {
            _transform = transform;
        }

        private void Start()
        {
            _startPosition = _transform.position;
        }

        private Vector3 CalculateEndPoint()
        {
            return Physics.Raycast(_transform.position, Vector3.right * MAX_DISTANCE, out RaycastHit hit, MAX_DISTANCE, groundLayerMask.value)
                ? hit.point
                : _transform.position;
        }
        
        public void Move()
        {
            print(CalculateEndPoint());
            _transform.DOMove(CalculateEndPoint(), moveDuration).SetEase(Ease.Linear);
        }
        
        public void Reset()
        {
            _transform.position = _startPosition;
        }
    }
}