using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Dungeon_Part
{
    public class DungeonPartWithBridge: DungeonPart
    {
        [SerializeField] private GameObject bridge;
        [SerializeField] private Transform[] bridgePositions;

        private Vector3[] _positions;
        private int _positionsCount;

        private void Awake()
        {
            _positions = bridgePositions.Select(transform1 => transform1.localPosition).ToArray();
            _positionsCount = _positions.Length;
        }

        private void OnEnable()
        {
            int index = Random.Range(0, _positionsCount);
            bridge.transform.localPosition = _positions[index];
        }
    }
}