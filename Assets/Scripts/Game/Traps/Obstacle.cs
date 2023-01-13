using Game.Interfaces;
using UnityEngine;

namespace Game.Traps
{
    public class Obstacle : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IObstacleVisitor component))
            {
                OnObstacleVisit(component);
            }
        }

        protected virtual void OnObstacleVisit(IObstacleVisitor component)
        {
            component.ObstacleVisit();
        }
    }
}