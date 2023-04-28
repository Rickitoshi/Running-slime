using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Dungeon
{
    [CreateAssetMenu(fileName = "Biome", menuName = "Dungeon/Biome", order = 0)]
    public class DungeonBiomes : ScriptableObject
    {
        [SerializeField] private DungeonPart startPart;
        [SerializeField] private DungeonPart[] middleParts;
        [SerializeField] private DungeonPart endPart;

        private List<DungeonPart> _partsPool;
        private DungeonPart _bufferedPart;
        
        public void Initialize(Transform parent)
        {
            _partsPool.Add(InstantiateDungeonPart(startPart, parent));
            
            foreach (var part in middleParts)
            {
                _partsPool.Add(InstantiateDungeonPart(part, parent));
            }
            
            _partsPool.Add(InstantiateDungeonPart(endPart, parent));
        }

        public DungeonPart GetRandomMeddleDungeonPart()
        {
            if (_partsPool.Count <= 0)
            {
                UnityEngine.Debug.LogError("Impossible to take an object from the pool because there are no objects in the pool");
                return null;
            }
                
            _bufferedPart = _partsPool[Random.Range(1, _partsPool.Count-1 )];
            _partsPool.Remove(_bufferedPart);
            return _bufferedPart;
        }

        public DungeonPart GetDungeonPart(int index)
        {
            if (_partsPool.Count <= 0)
            {
                UnityEngine.Debug.LogError("Impossible to take an object from the pool because there are no objects in the pool");
                return null;
            }
            
            _bufferedPart = _partsPool[index];
            _partsPool.Remove(_bufferedPart);
            return _bufferedPart;
        }

        public void BackDungeonPart(DungeonPart part)
        {
            part.Deactivate();
           // _partsPool.
        }

        private DungeonPart InstantiateDungeonPart(DungeonPart part,Transform parent)
        {
            _bufferedPart = Instantiate(part, parent);
            _bufferedPart.Deactivate();
            return _bufferedPart;
        }
    }
}