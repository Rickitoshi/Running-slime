using System;
using UnityEngine;

namespace Game.Buildings
{
    public class HintStone : MonoBehaviour
    {
        private MeshFilter _meshFilter;

        private void Awake()
        {
            _meshFilter = GetComponentInChildren<MeshFilter>();
        }

        public void Initialize(Mesh view)
        {
            _meshFilter.mesh = view;
        }
    }
}