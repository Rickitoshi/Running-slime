using System;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerMoveSystem playerMoveSystem;
        [SerializeField] private PlayerAnimationController _animationController;

        private InputHandler _inputHandler;
        private PlayerConfig _playerConfig;

        [Inject]
        private void Construct(InputHandler inputHandler, PlayerConfig playerConfig)
        {
            _inputHandler = inputHandler;
            _playerConfig = playerConfig;
        }

        private void Start()
        {
            playerMoveSystem.Initialize(_playerConfig);
        }

        private void Update()
        {
            if(!playerMoveSystem.IsGrounded) return;
            
            if (_inputHandler.IsTouch)
            {
                playerMoveSystem.Jump();
                _animationController.SetJump();
            }

            if(playerMoveSystem.IsStrafe) return;
            
            if (_inputHandler.IsLeftSwipe)
            {
                playerMoveSystem.Strafe(PlayerMoveSystem.StrafeDirection.Left);
            }
            
            if (_inputHandler.IsRightSwipe)
            {
                playerMoveSystem.Strafe(PlayerMoveSystem.StrafeDirection.Right);
            }
            
        }
    }
}