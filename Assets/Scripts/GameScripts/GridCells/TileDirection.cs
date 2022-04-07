using System.Collections.Generic;
using UnityEngine;

namespace GameScripts.GridCells
{
    public class TileDirection
    {
        private List<FillTile> _fillTiles = new List<FillTile>();
        public Vector2Int FillDirection { get; }
        public Color Color { get; }
        public IEnumerable<FillTile> GetFillTiles() => _fillTiles;
        public TileDirection(Vector2Int direction, Color color)
        {
            FillDirection = direction;
            Color = color;
        }
        public void AddTileFill(FillTile fillTile) => _fillTiles.Add(fillTile);
    }
}