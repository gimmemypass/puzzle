using System;
using DG.Tweening;
using GameScripts.GridCells;
using UnityEngine;

namespace GameScripts.GameCore
{
    [RequireComponent(typeof(MeshRenderer))]
    public class FillTileView : CellView
    {
        private MeshRenderer _meshRenderer;
        public FillTile FillTile { get; set; }
        
        public Tween TweenColor(Color color, float duration)
        {
            return _meshRenderer.material.DOColor(color, duration);
        }

        public void InitializeColor(Color color) => _meshRenderer.material.color = color;

        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
        }
    }
}