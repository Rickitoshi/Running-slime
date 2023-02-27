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
        
        private void Subscribe()
        {
            _signalBus.Subscribe<ChangeGameStateSignal>(OnChangeGameState);
            healthSystem.OnDie += OnDie;
            _inputHandler.OnHorizontalSwap += OnHorizontalSwipe;
            _inputHandler.OnClick += OnClick;
        }

        private void Unsubscribe()
        {
            _signalBus.Unsubscribe<ChangeGameStateSignal>(OnChangeGameState);
            healthSystem.OnDie -= OnDie;
            _inputHandler.OnHorizontalSwap -= OnHorizontalSwipe;
            _inputHandler.OnClick -= OnClick;
        }

        private void OnHorizontalSwipe(float normalizeX)
        {
            if(!healthSystem.IsAlive) return;
            if(playerMoveSystem.IsJumping) return;
            
            if (normalizeX < 0)
            {
                playerMoveSystem.Strafe(PlayerMoveSystem.StrafeDirection.Left);
                animationController.SetJump();
            }
            else
            {
                playerMoveSystem.Strafe(PlayerMoveSystem.StrafeDirection.Right);
                animationController.SetJump();
            }
        }

        private void OnClick()
        {
            if(!healthSystem.IsAlive) return;
            if(playerMoveSystem.IsJumping) return;
            
            playerMoveSystem.JumpForward();
            animationController.SetJump();
            _signalBus.Fire<ScoreChangedSignal>();
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