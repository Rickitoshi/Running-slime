using UnityEngine;

namespace Game.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        private const string JUMP = "Jump";
        private const string LEFT_STRAFE = "LeftStrafe";
        private const string RIGHT_STRAFE = "RightStrafe";

        private readonly int Jump = Animator.StringToHash(JUMP);
        private readonly int LeftStrafe = Animator.StringToHash(LEFT_STRAFE);
        private readonly int RightStrafe = Animator.StringToHash(RIGHT_STRAFE);

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
            _animator.SetTrigger(Jump);
        }
        
        public void SetStrafe(PlayerMoveSystem.StrafeDirection direction)
        {
            if (direction == PlayerMoveSystem.StrafeDirection.Left)
            {
                _animator.SetTrigger(LeftStrafe);
            }
            else
            {
                _animator.SetTrigger(RightStrafe);
            }
        }
    }
}