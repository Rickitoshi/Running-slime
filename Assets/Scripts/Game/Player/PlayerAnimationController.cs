using UnityEngine;

namespace Game.Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        private const string JUMP = "Jump";

        private readonly int Jump = Animator.StringToHash(JUMP);

        [SerializeField] private Animator _animator;

        public void SetJump()
        {
            _animator.SetTrigger(Jump);
        }
        
    }
}