using UnityEngine;
using Game.Player;
using DG.Tweening;
using Signals;
using Unity.Mathematics;
using Zenject;

public class PlayerMoveSystem : MonoBehaviour
{
    [SerializeField] private LayerMask groundLayerMask;
    
    public bool IsActive
    {
        get => _isActive;
        set
        {
            if (value == _isActive) return;

            _isActive = value;
            if (_isActive)
            {
                DOTween.Play(transform);
            }
            else
            {
                DOTween.Pause(transform);
            }
        }
    }
    
    public bool IsJumping { get; private set; }

    [Inject] private SignalBus _signalBus;
    
    private float _jumpDuration;
    private float _jumpDistance;
    private float _jumpHeight;
    private float _rotateDuration;

    private Transform _transform;
    private Vector3 _startPosition;
    private Vector3 _velocity;
    private bool _isActive;
    private Tween _strafeTween;
    private LookDirection _currentLookDirection;

    private void Awake()
    {
        _transform = GetComponent<Transform>();
    }

    private void Start()
    {
        _startPosition = _transform.position;
    }

    public void Initialize(PlayerConfig playerConfig)
    {
        _jumpDistance = playerConfig.JumpDistance;
        _jumpDuration = playerConfig.JumpDuration;
        _jumpHeight = playerConfig.JumpHeight;
        _rotateDuration = playerConfig.RotateDuration;
    }

    public void Strafe(StrafeDirection direction)
    {

        if (direction == StrafeDirection.Right)
        {
            if(!IsCanStrafe(Vector3.right)) return;
            
            Rotate(LookDirection.Right);
            Jump(CalculateJumpEndPoint(Vector3.right));
        }
        else
        {
            if(!IsCanStrafe(Vector3.left)) return;
            
            Rotate(LookDirection.Left);
            Jump(CalculateJumpEndPoint(Vector3.left));
        }
    }
    
    public void JumpForward()
    {
        Rotate(LookDirection.Forward);
        Jump(CalculateJumpEndPoint(Vector3.forward));
        _signalBus.Fire(new PlayerJumpSignal(_transform.position.z));
    }
    
    private void Jump(Vector3 endPoint)
    {
        IsJumping = true;
        _transform.DOJump(endPoint, _jumpHeight, 1, _jumpDuration).SetEase(Ease.InOutCubic).OnComplete(
            () =>
            {
                IsJumping = false;
            });
    }
    
    private void Rotate(LookDirection direction)
    {
        if (_currentLookDirection == direction) return;

        DOTween.Kill(_transform);
        _currentLookDirection = direction;
        _transform.DORotate(new Vector3(0, (int)direction, 0), _rotateDuration);
    }

    private Vector3 CalculateJumpEndPoint(Vector3 direction)
    {
        Vector3 rayStartPoint = GetPlayerPosition() + direction * _jumpDistance;
        return Physics.Raycast(rayStartPoint, Vector3.down, out RaycastHit hit, 5f, groundLayerMask.value)
            ? hit.point
            : _transform.position;
    }
    
    private bool IsCanStrafe(Vector3 direction)
    {
        return !Physics.Raycast(GetPlayerPosition(), direction, out RaycastHit hit, _jumpDistance,
            groundLayerMask.value);
    }

    private Vector3 GetPlayerPosition()
    {
        return _transform.position + Vector3.up;
    }

    public void Reset()
    {
        DOTween.Kill(_transform);
        IsJumping = false;
        _transform.position = _startPosition;
        _transform.rotation = Quaternion.identity;
    }
    
    public enum StrafeDirection
    {
        Left,
        Right
    }

    private enum LookDirection
    {
        Left=-90,
        Forward=0,
        Right=90
    }
}
