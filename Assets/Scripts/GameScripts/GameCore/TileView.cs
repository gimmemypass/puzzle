using System;
using Cysharp.Threading.Tasks;
using GameScripts.GridCells;
using UnityEngine;

namespace GameScripts.GameCore
{
    public class TileView : CellView
    {
        [SerializeField] private TileArrowView[] _tileArrows; //left, down, right, up 
        public Tile Tile { get; set; }

        private void Awake()
        {
            foreach (var tileArrowView in _tileArrows)
            {
                tileArrowView.gameObject.SetActive(false);
            }
        }

        public void AddDirection(TileDirection direction)
        {
            ArrowByDirection(direction).gameObject.SetActive(true);
        }

        public async UniTask DisableArrow(TileDirection direction)
        {
            await ArrowByDirection(direction).Disable();
        }

        private TileArrowView ArrowByDirection(TileDirection direction)
        {

            if (direction.FillDirection == Vector2Int.left)
                return _tileArrows[0];
            if (direction.FillDirection == Vector2Int.down)
                return _tileArrows[1];
            if (direction.FillDirection == Vector2Int.right)
                return _tileArrows[2];
            if (direction.FillDirection == Vector2Int.up)
                return _tileArrows[3];

            throw new ArgumentException("magnitude of the direction must be equal 1");
        }
    }
}