using System.Collections.Generic;
using Signals;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class DungeonManager : MonoBehaviour
{
    [SerializeField] private DungeonPart[] partsPool;
    [SerializeField] private int playParts = 4;

    [Inject] private SignalBus _signalBus;
    
    private Queue<DungeonPart> _currentRoad;
    private List<DungeonPart> _instantiatedDungeonParts;
    private DungeonPart _partObject;
    private Vector3 _partPosition;

    private void Awake()
    {
        _instantiatedDungeonParts = new List<DungeonPart>(partsPool.Length);
        _currentRoad = new Queue<DungeonPart>();
        SetDefaultPosition();
    }

    private void Start()
    {
        Initialize();
    }
    
    private void OnDestroy()
    {
       Unsubscribe();
    }

    private void Initialize()
    {
        InstantiateRoadPool();
        Subscribe();
        InitializeRoad();
    }

    private void InitializeRoad()
    {
        for (int i = 0; i < playParts; i++)
        {
            TakePart();
        }
    }

    private void SetDefaultPosition()
    {
        _partPosition = new Vector3(0, 0, 0);
    }
    
    private void TakePart()
    {
        if (_instantiatedDungeonParts.Count == 0) 
            return;
        
        int index = Random.Range(0, _instantiatedDungeonParts.Count );
        _partObject = _instantiatedDungeonParts[index];
        _instantiatedDungeonParts.Remove(_partObject);
        _partObject.Activate(_partPosition);
        _partPosition.z += _partObject.Lenght;
        _currentRoad.Enqueue(_partObject);
    }

    private void ReturnPart()
    {
        if (_currentRoad.Count == 0)
            return;
        
        _partObject = _currentRoad.Dequeue();
        _partObject.Deactivate();
        _instantiatedDungeonParts.Add(_partObject);
    }

    private void UpdateDungeon(PlayerJumpSignal signal)
    {
        if (_partPosition.z - signal.PositionZ <= 13)
        {
            TakePart();
        }

        if (signal.PositionZ - (_currentRoad.Peek().transform.position.z + _currentRoad.Peek().Lenght * 0.5f) >= 10)
        {
            ReturnPart();
        }
    }

    private void InstantiateRoadPool()
    {
        foreach (var part in partsPool)
        {
            _partObject = Instantiate(part, transform);
            _partObject.Deactivate();
            _instantiatedDungeonParts.Add(_partObject);
        }
    }

    private void Subscribe()
    {
        _signalBus.Subscribe<PlayerJumpSignal>(UpdateDungeon);
    }
    
    private void Unsubscribe()
    {
        _signalBus.Unsubscribe<PlayerJumpSignal>(UpdateDungeon);
    }

    public void Restart()
    {
        int size = _currentRoad.Count;
        for (int i = 0; i < size; i++)
        {
            ReturnPart();
        }

        SetDefaultPosition();
        InitializeRoad();
    }
}