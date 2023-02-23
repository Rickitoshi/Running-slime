using Game.Managers;
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
        private SignalBus _signalBus;

        [Inject]
        private void Construct(InputHandler inputHandler, PlayerConfig playerConfig,SignalBus signalBus)
        {
            _inputHandler = inputHandler;
            _playerConfig = playerConfig;
            _signalBus = signalBus;
        }

        private void Awake()
        {
            Subscribe();
        }
        
        private void OnDestroy()
        {
           Unsubscribe(); 
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
                playerMoveSystem.JumpForward();
                animationController.SetJump();
                _signalBus.Fire<ScoreChangedSignal>();
            }
            
            if (_inputHandler.IsLeftSwipe)
            {
                playerMoveSystem.Strafe(PlayerMoveSystem.StrafeDirection.Left);
                animationController.SetJump();
            }
            
            if (_inputHandler.IsRightSwipe)
            {
                playerMoveSystem.Strafe(PlayerMoveSystem.StrafeDirection.Right);
                animationController.SetJump();
            }
        }

        private void Subscribe()
        {
            _signalBus.Subscribe<ChangeGameStateSignal>(OnChangeGameState);
            healthSystem.OnDie += OnDie;
        }

        private void Unsubscribe()
        {
            _signalBus.Unsubscribe<ChangeGameStateSignal>(OnChangeGameState);
            healthSystem.OnDie -= OnDie;
        }

        private void OnChangeGameState(ChangeGameStateSignal signal)
        {
            if (signal.State == GameState.Menu)
            {
                Restart();
            }
        }
        
        private void OnDie()
        {
            destructionSystem.Explosion(1);
            _signalBus.Fire<OnPlayerDieSignal>();
        }

        private void Restart()
        {
            healthSystem.Reset();
            destructionSystem.Recovery();
            playerMoveSystem.Reset();
        }
        
    }
}