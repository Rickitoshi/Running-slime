using UnityEngine;

namespace Game.Buildings
{
    public class HintSign : MonoBehaviour
    {
       [SerializeField] private MeshFilter meshFilter;

       public void Initialize(Mesh view)
        {
            meshFilter.mesh = view;
        }
    }
}