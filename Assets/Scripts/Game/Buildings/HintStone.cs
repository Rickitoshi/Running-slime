using UnityEngine;

namespace Game.Buildings
{
    public class HintStone : MonoBehaviour
    {
       [SerializeField] private MeshFilter meshFilter;

       public void Initialize(Mesh view)
        {
            meshFilter.mesh = view;
        }
    }
}