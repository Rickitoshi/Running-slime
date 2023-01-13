using UnityEngine;

namespace Game.Player
{
    public class GroundCheckSystem : MonoBehaviour
    {
        [SerializeField] private float groundDistance = 0.3f;
        [SerializeField] private LayerMask layerMask;

        public bool IsGrounded { get; private set; }

        private void FixedUpdate()
        {
            var position = transform.position;
            Ray ray = new Ray(position, Vector3.down);
            IsGrounded = Physics.Raycast(ray, groundDistance, layerMask.value);
        }
    }
}