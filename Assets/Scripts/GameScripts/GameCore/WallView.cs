using System;
using UnityEngine;

namespace GameScripts.GameCore
{
    [RequireComponent(typeof(MeshRenderer))]
    public class WallView : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;

        public Material TopSharedMaterial => _meshRenderer.sharedMaterials[1];
        public Material DownSharedMaterial => _meshRenderer.sharedMaterials[0];
        private Material TopMaterial => _meshRenderer.materials[1];
        private Material DownMaterial => _meshRenderer.materials[0];

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }
    }
}