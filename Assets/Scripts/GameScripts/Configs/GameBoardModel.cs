using GameScripts.GridCells;

namespace GameScripts.Configs
{
    public class GameBoardModel
    {
        private Cell[,] _grid;
        public Cell[,] GetGridCopy => _grid.Clone() as Cell[,];
        public GameBoardModel(Cell[,] grid)
        {
            _grid = grid;
        }
    }
}