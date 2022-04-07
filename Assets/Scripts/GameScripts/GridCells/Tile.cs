using System;
using System.Linq;
using UnityEngine;

namespace GameScripts.GridCells
{
    public class Tile : Cell
    {
        public Color TileColor { get; }
        public TileDirection[] TileDirections { get; private set; }

        public Tile(TileDirection[] fillDirections, Color tileColor)
        {
            TileColor = tileColor;
            InitializeDirections(fillDirections);
        }

        private void InitializeDirections(TileDirection[] fillDirections)
        {
            if (fillDirections.Length > 4)
                throw new ArgumentException("Count of the directions must be less or equal 4");
            if (fillDirections.Length != fillDirections.Select(d => d.FillDirection).Distinct().Count())
                throw new ArgumentException("Tile's fill directions must be different");
            if (fillDirections.Count(d => Math.Abs(d.FillDirection.magnitude - 1) > float.Epsilon) != 0)
                throw new ArgumentException("Magnitude of the fill direction must be equal 1");
            TileDirections = fillDirections;
        }

        public override void Accept(ICellVisitor visitor)
        {
            visitor.Visit(this);
        }

        public override string ToString()
        {
            return "T";
        }
    }
}