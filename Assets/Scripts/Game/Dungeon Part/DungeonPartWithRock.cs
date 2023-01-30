using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Dungeon_Part
{
    public class DungeonPartWithRock: DungeonPart
    {
        [SerializeField] private GameObject[] obstacles;

        private int _obstaclesCount;
        private GameObject _currentObstacle;
        
        private void Awake()
        {
            _obstaclesCount = obstacles.Length;

            foreach (var obstacle in obstacles)
            {
                obstacle.SetActive(false);
            }
        }

        private void OnEnable()
        {
            int index = Random.Range(0, _obstaclesCount);
            _currentObstacle = obstacles[index];
            _currentObstacle.SetActive(true);
        }

        private void OnDisable()
        {
            _currentObstacle.SetActive(false);
        }
    }
}