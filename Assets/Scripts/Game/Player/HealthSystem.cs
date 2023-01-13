using System;
using Game.Interfaces;
using UnityEngine;

public class HealthSystem : MonoBehaviour, IObstacleVisitor
{
    public bool IsAlive { get; private set; }
    
    public event Action OnDie;

    private void Start()
    {
        IsAlive = true;
    }

    [ContextMenu("Die")]
    private void Die()
    {
        IsAlive = false;
        OnDie?.Invoke();
    }
    
    public void Reset()
    {
        IsAlive = true;
    }

    public void ObstacleVisit()
    {
        if (!IsAlive) return;
        
        Die();
    }
}
