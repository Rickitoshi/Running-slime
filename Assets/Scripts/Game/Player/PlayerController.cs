using Signals;
using UnityEngine;
using Zenject;

namespace Game.Player
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerMoveSystem playerMoveSystem;
        [SerializeField] private PlayerAnimationController animationController;
        [SerializeField] private DestructionSystem destructionSystem;
        [SerializeField] private HealthSystem healthSystem;
        
        private InputHandler _inputHandler;
        private PlayerConfig _playerConfig;

        [Inject]
        private void Construct(InputHandler inputHandler, PlayerConfig playerConfig)
        {
            _inputHandler = inputHandler;
            _playerConfig = playerConfig;
        }

        private void Awake()
        {
            healthSystem.OnDie += OnDie;
        }
        
        private void OnDestroy()
        {
            healthSystem.OnDie -= OnDie;
        }

        private void Start()
        {
            playerMoveSystem.Initialize(_playerConfig);
        }

        private void Update()
        {
            if(!healthSystem.IsAlive) return;
            if(playerMoveSystem.IsJumping) return;
            
            if (_inputHandler.IsTouch)
            {
                playerMoveSystem.Jump();
                animationController.SetJump();
            }

            if(playerMoveSystem.IsStrafe) return;
            
            if (_inputHandler.IsLeftSwipe)
            {
                playerMoveSystem.Strafe(PlayerMoveSystem.StrafeDirection.Left);
                animationController.SetStrafe(PlayerMoveSystem.StrafeDirection.Left);
            }
            
            if (_inputHandler.IsRightSwipe)
            {
                playerMoveSystem.Strafe(PlayerMoveSystem.StrafeDirection.Right);
                animationController.SetStrafe(PlayerMoveSystem.StrafeDirection.Right);
            }
        }

        private void OnDie()
        {
            destructionSystem.Explosion(1);
        }

        public void Restart()
        {
            healthSystem.Reset();
            destructionSystem.Recovery();
            playerMoveSystem.Reset();
        }
        
    }
}