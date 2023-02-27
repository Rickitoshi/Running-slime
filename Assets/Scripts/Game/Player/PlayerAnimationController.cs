using System;
using Unity.VisualScripting;
using UnityEngine;

namespace Game.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        private const string JUMP = "Jump";

        private readonly int Jump = Animator.StringToHash(JUMP);

        [SerializeField] private Animator _animator;
        
        public bool IsActive
        {
            get => _isActive;
            set
            {
                if (value == _isActive) return;

                _isActive = value;
                _animator.enabled = _isActive;
            }
        }

        private bool _isActive;

        public void SetJump()
        {
            _animator.Rebind();
            _animator.SetTrigger(Jump);
        }
    }
}