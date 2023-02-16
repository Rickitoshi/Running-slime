using System;
using UnityEngine;
using Game.Player;
using DG.Tweening;
using Signals;
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
    public bool IsStrafe { get; private set; }

    [Inject] private SignalBus _signalBus;
    
    private float _jumpDuration;
    private float _jumpDistance;
    private float _jumpHeight;
    private float _strafeDuration;
    private float _strafeDistance;
    
    private Transform _transform;
    private Vector3 _startPosition;
    private Vector3 _velocity;
    private bool _isActive;
    private Tween _strafeTween;

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
        _strafeDuration = playerConfig.StrafeDuration;
        _strafeDistance = playerConfig.StrafeDistance;
    }

    public void Strafe(StrafeDirection direction)
    {
        IsStrafe = true;
        
        if (direction == StrafeDirection.Right)
        {
            _strafeTween = _transform.DOMoveX(_transform.position.x + _strafeDistance, _strafeDuration);
        }
        else
        {
            _strafeTween = _transform.DOMoveX(_transform.position.x - _strafeDistance, _strafeDuration);
        }

        _strafeTween.SetEase(Ease.OutQuad);
        _strafeTween.OnComplete(() => { IsStrafe = false; });
    }
    
    public void Jump()
    {
        IsJumping = true;
        DOTween.Complete(_transform);
        _transform.DOJump(CalculateJumpEndPoint(), _jumpHeight, 1, _jumpDuration).SetEase(Ease.InOutCubic).OnComplete(
            () =>
            {
                _signalBus.Fire(new PlayerJumpSignal(_transform.position.z));
                IsJumping = false;
            });
    }

    private Vector3 CalculateJumpEndPoint()
    {
        Vector3 rayStartPoint = _transform.position +  Vector3.forward * _jumpDistance;
        return Physics.Raycast(rayStartPoint, Vector3.down, out RaycastHit hit, 10f, groundLayerMask.value)
            ? hit.point
            : _transform.position;
    }

    public void Reset()
    {
        DOTween.Kill(_transform);
        IsJumping = false;
        _transform.position = _startPosition;
    }
    
    public enum StrafeDirection
    {
        Left,
        Right
    }
}
