using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameScripts.Configs;
using GameScripts.GridCells;
using UnityEngine;
namespace GameScripts.GameCore
{
    public class GameBoardController
    {
        public Cell[,] Grid { get; }

        public GameBoardController(GameBoardModel boardModel)
        {
            Grid = boardModel.GetGridCopy;
        }

        public bool TryMove(Tile tile, Vector2 moveDirection, out Vector2Int newPos)
        {
            int startRow, startColumn;
            (startRow, startColumn) = Grid.CoordinatesOf(tile);
            
            int rowMove = Math.Sign(moveDirection.y);
            int columnMove = Math.Sign(moveDirection.x);

            var finalRow = startRow + rowMove;
            var finalColumn = startColumn + columnMove;
            var moveStep = 1;
            while (IsCellTypeOf<EmptyCell>(finalRow, finalColumn))
            {
                finalRow += rowMove;
                finalColumn += columnMove;
                moveStep++;
            }

            finalRow -= rowMove;
            finalColumn -= columnMove;
            moveStep--;

            var hasMoved = moveStep > 0;
            if (hasMoved)
            {
                //Swap tile cell with empty cell
                (Grid[startRow, startColumn], Grid[finalRow, finalColumn]) =
                    (Grid[finalRow, finalColumn], Grid[startRow, startColumn]);
            }

            newPos = new Vector2Int(finalRow, finalColumn);
            return moveStep > 0;
        }

        public bool IsGameEnded()
        {
            var emptyCellsCount = GetCells<EmptyCell>().Count();
            var fillingCellsCount = 0;
            var visited = new List<Cell>();

            foreach (var tile in GetCells<Tile>())
            {
                foreach (var direction in tile.TileDirections)
                {
                    int startRow, startColumn;
                    (startRow, startColumn) = Grid.CoordinatesOf(tile);
                    int rowMove = Math.Sign(direction.FillDirection.y);
                    int columnMove = Math.Sign(direction.FillDirection.x);
                    var finalRow = startRow + rowMove;
                    var finalColumn = startColumn + columnMove;
                    
                    while (IsCellTypeOf<EmptyCell>(finalRow, finalColumn))
                    {
                        if (!visited.Contains(Grid[finalRow, finalColumn]))
                        {
                            fillingCellsCount++;
                            visited.Add(Grid[finalRow, finalColumn]); 
                        }
                        finalRow += rowMove;
                        finalColumn += columnMove;
                    }

                }
            }
            return emptyCellsCount <= fillingCellsCount;
        }

        public void EndGame()
        {
            foreach (var tile in GetCells<Tile>())
            {
                foreach (var direction in tile.TileDirections)
                {
                    int startRow, startColumn;
                    (startRow, startColumn) = Grid.CoordinatesOf(tile);
                    int rowMove = Math.Sign(direction.FillDirection.y);
                    int columnMove = Math.Sign(direction.FillDirection.x);
                    var finalRow = startRow + rowMove;
                    var finalColumn = startColumn + columnMove;
                    
                    while (!IsCellTypeOf<Wall>(finalRow, finalColumn))
                    {
                        if (Grid[finalRow, finalColumn] is EmptyCell)
                        {
                            var fillTile = new FillTile();
                            Grid[finalRow, finalColumn] = fillTile;
                            direction.AddTileFill(fillTile);
                        }
                        else if(Grid[finalRow, finalColumn] is FillTile fillTile)
                        {
                            direction.AddTileFill(fillTile); 
                        }
                        finalRow += rowMove;
                        finalColumn += columnMove;
                    }
                }
            }
        }

        public IEnumerable<T> GetCells<T>() where T : Cell
        {
            var emptyCells = new List<T>();
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    if (Grid[i, j] is T)
                        emptyCells.Add(Grid[i, j] as T);
                }
            }

            return emptyCells;
        }


        private bool IsWithinBounds(int rowMove, int columnMove)
        {
            return rowMove >= 0 && rowMove < Grid.GetLength(0) && columnMove >= 0 && columnMove < Grid.GetLength(1);
        }

        private bool IsCellTypeOf<T>(int rowMove, int columnMove) where T : Cell
        {
            return IsWithinBounds(rowMove, columnMove) && Grid[rowMove, columnMove] is T;
        }

        public override string ToString()
        {
            var matrixString = new StringBuilder();
            for (int i = 0; i < Grid.GetLength(0); i++)
            {
                var row = new StringBuilder();
                for (int j = 0; j < Grid.GetLength(1); j++)
                {
                    row.Append(Grid[i,j].ToString());
                }

                matrixString.Append($"{row}\n");
            }
            return matrixString.ToString();
        }
    }
}