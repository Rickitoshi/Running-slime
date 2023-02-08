using System;
using DG.Tweening;
using Game.Player;
using UnityEngine;

namespace Game.Buildings
{
    public class DungeonButton : MonoBehaviour
    { 
        [SerializeField] private float endPositionY;
        [SerializeField] private float moveDuration = 1;

        public event Action<ButtonType> OnPressed;
        
        private ButtonType _type;
        private MeshFilter _meshFilter;
        private Transform _transform;
        private Vector3 _startPosition;
        private bool _isActive;
        
        private void Awake()
        {
            _transform = transform;
            _meshFilter = GetComponentInChildren<MeshFilter>();
            _startPosition = _transform.position;
            _isActive = true;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!_isActive) return;
            
            if (other.GetComponent<PlayerController>())
            {
                _transform.DOMoveY(endPositionY, moveDuration);
                OnPressed?.Invoke(_type);
            }
        }

        public void Initialize(ButtonType type, Mesh view)
        {
            _type = type;
            _meshFilter.mesh = view;
        }
        
        public void Reset()
        {
            _transform.position = _startPosition;
            _isActive = true;
        }
    }

    public enum ButtonType
    {
        Circle,
        Cross,
        Plus
    }
}