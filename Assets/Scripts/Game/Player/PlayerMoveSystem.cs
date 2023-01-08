using UnityEngine;
using Game.Player;
using DG.Tweening;

[RequireComponent(typeof(GroundCheckSystem))]
public class PlayerMoveSystem : MonoBehaviour
{
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

    public bool IsGrounded => _groundCheckSystem.IsGrounded;

    private float _jumpDuration;
    private float _jumpDistance;
    private float _jumpHeight;
    private float _strafeDuration;
    private float _strafeDistance;

    private GroundCheckSystem _groundCheckSystem;
    private Vector3 _velocity;
    private bool _isActive;

    private void Awake()
    {
        _groundCheckSystem = GetComponent<GroundCheckSystem>();
    }

    public void Initialize(PlayerConfig playerConfig)
    {
        _jumpDistance = playerConfig.JumpDistance;
        _jumpDuration = playerConfig.JumpDuration;
        _jumpHeight = playerConfig.JumpHeight;
        _strafeDuration = playerConfig.StrafeDuration;
        _strafeDistance = playerConfig.StrafeDistance;
    }

    
    private void Strafe()
    {
        transform.DOMoveX(transform.position.x + _strafeDistance, _strafeDuration);
    }

    public void Jump()
    {
        DOTween.Complete(transform);
        transform.DOJump(transform.position + Vector3.forward * _jumpDistance, _jumpHeight, 1, _jumpDuration).SetEase(Ease.InOutCubic);
    }

    public void ResetBehaviour()
    {
        DOTween.Kill(transform);
    }
}
