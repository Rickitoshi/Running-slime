using Game.Interfaces;
using UnityEngine;

namespace Game.Items
{
    public abstract class BaseItem : MonoBehaviour
    {
        public void Activate(Vector3 position)
        {
            transform.position = position;
            gameObject.SetActive(true);
        }

        public void Deactivate()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out IItemVisitor visitor))
            {
                OnVisit(visitor);
            }
        }

        protected abstract void OnVisit(IItemVisitor visitor);
    }
}