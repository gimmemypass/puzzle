using System.Collections.Generic;
using System.Linq;
using Configs;
using GameScripts.GameCore;
using GameScripts.GridCells;
using UnityEngine;

namespace GameScripts.Configs
{
    public class GameBoardProvider : IGameBoardProvider
    {
        private readonly GameConfig _gameConfig;

        public GameBoardProvider(
            GameConfig gameConfig
            )
        {
            _gameConfig = gameConfig;
        }
        private Color TileColor => _gameConfig.TileColor;
        //disgusting solution to create deep copy of levels each time
        private List<GameBoardModel> Boards => new List<GameBoardModel>
        {
            new GameBoardModel( new Cell[,]
            {
                {new Wall(),new Wall(), new Wall(), new Wall(), new Wall(), new Wall()},
                {new Wall(),new Tile(new [] {new TileDirection(Vector2Int.up, TileColor), new TileDirection(Vector2Int.left, TileColor)}, TileColor), new EmptyCell(), new EmptyCell(), new EmptyCell(), new Wall()},
                {new Wall(),new EmptyCell(), new Wall(), new Wall(), new Wall(), new Wall()},
                {new Wall(),new EmptyCell(), new EmptyCell(), new Tile(new [] {new TileDirection(Vector2Int.right, TileColor)}, TileColor), new Wall(), new Wall()},
                {new Wall(),new EmptyCell(), new Wall(), new Tile(new [] {new TileDirection(Vector2Int.up, TileColor)}, TileColor), new Wall(), new Wall()},
                {new Wall(),new Wall(), new Wall(), new Wall(), new Wall(), new Wall()},
            }),
            new GameBoardModel( new Cell[,]
            {
                {new Wall(),new Wall(), new Wall(), new Wall(), new Wall(), new Wall(), new Wall()},
                {new Wall(),new Wall(), new Wall(), new Tile(new [] {new TileDirection(Vector2Int.down, TileColor)}, TileColor), new Wall(), new Wall(), new Wall()},
                {new Wall(),new Wall(), new Wall(), new EmptyCell(), new Wall(), new Wall(), new Wall()},
                {new Wall(),new EmptyCell(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new Tile(new [] {new TileDirection(Vector2Int.right, TileColor)}, TileColor), new Wall()},
                {new Wall(),new Wall(), new Wall(), new EmptyCell(), new Wall(), new Wall(), new Wall()},
                {new Wall(),new Wall(), new Wall(), new EmptyCell(), new Wall(), new Wall(), new Wall()},
                {new Wall(),new Wall(), new Wall(), new Wall(), new Wall(), new Wall(), new Wall()}
            }),
            new GameBoardModel( new Cell[,]
            {
                {new Wall(),new Wall(), new Wall(), new Wall(), new Wall(), new Wall()},
                {new Wall(),new Tile(new [] {new TileDirection(Vector2Int.down, TileColor)}, TileColor), new EmptyCell(), new Tile(new [] {new TileDirection(Vector2Int.down, TileColor), new TileDirection(Vector2Int.right,TileColor)}, TileColor), new Wall(), new Wall()},
                {new Wall(),new Wall(), new EmptyCell(), new Wall(), new Wall(), new Wall()},
                {new Wall(),new Wall(), new EmptyCell(), new EmptyCell(), new EmptyCell(), new Wall()},
                {new Wall(),new Wall(), new Wall(), new EmptyCell(), new Wall(), new Wall()},
                {new Wall(),new Wall(), new Wall(), new Tile(new [] {new TileDirection(Vector2Int.left, TileColor)}, TileColor), new Wall(), new Wall()},
                {new Wall(),new Wall(), new Wall(), new Wall(), new Wall(), new Wall()}
            }),
            new GameBoardModel( new Cell[,]
            {
                {new Wall(),new Wall(),new Wall(), new Wall(), new Wall(), new Wall()},
                {new Wall(),new Tile(new [] {new TileDirection(Vector2Int.down, TileColor)}, TileColor),new Wall(), new Wall(), new Wall(), new Wall()},
                {new Wall(),new Tile(new [] {new TileDirection(Vector2Int.down, TileColor), new TileDirection(Vector2Int.right, TileColor)}, TileColor), new EmptyCell(), new EmptyCell(), new EmptyCell(), new Wall()},
                {new Wall(),new Wall(),new Wall(), new EmptyCell(), new Wall(), new Wall()},
                {new Wall(),new Wall(),new Wall(), new EmptyCell(), new Wall(), new Wall()},
                {new Wall(),new Wall(),new Wall(), new Wall(), new Wall(), new Wall()},
            }),
            new GameBoardModel( new Cell[,]
            {
                {new Wall(),new Wall(),new Wall(), new Wall(), new Wall(), new Wall()},
                {new Wall(),new EmptyCell(),new EmptyCell(), new Wall(), new Wall(), new Wall()},
                {new Wall(),new EmptyCell(),new EmptyCell(), new Wall(), new Wall(), new Wall()},
                {new Wall(),new EmptyCell(),new EmptyCell(), new Wall(), new Wall(), new Wall()},
                {new Wall(),new EmptyCell(),new EmptyCell(), new Tile(new [] {new TileDirection(Vector2Int.down, TileColor)}, TileColor), new Tile(new [] {new TileDirection(Vector2Int.left, TileColor)}, TileColor), new Wall()},
                {new Wall(),new EmptyCell(),new EmptyCell(), new Wall(), new Wall(), new Wall()},
                {new Wall(),new EmptyCell(),new EmptyCell(), new Wall(), new Wall(), new Wall()},
                {new Wall(),new EmptyCell(),new EmptyCell(), new EmptyCell(), new Tile(new [] {new TileDirection(Vector2Int.right, TileColor), new TileDirection(Vector2Int.down, TileColor)}, TileColor), new Wall()},
                {new Wall(),new Wall(),new Wall(), new Wall(), new Wall(), new Wall()}
            }),
        };
        //sample
        // new GameBoardModel( new Cell[,]
        // {
        //     {new Wall(),new Wall(),new Wall(), new Wall(), new Wall(), new Wall(), new Wall()},
        //     {new Wall(),new Wall(),new Wall(), new Wall(), new Wall(), new Wall(), new Wall()},
        //     {new Wall(),new Wall(),new Wall(), new Wall(), new Wall(), new Wall(), new Wall()},
        //     {new Wall(),new Wall(),new Wall(), new Wall(), new Wall(), new Wall(), new Wall()},
        //     {new Wall(),new Wall(),new Wall(), new Wall(), new Wall(), new Wall(), new Wall()},
        //     {new Wall(),new Wall(),new Wall(), new Wall(), new Wall(), new Wall(), new Wall()},
        //     {new Wall(),new Wall(),new Wall(), new Wall(), new Wall(), new Wall(), new Wall()}
        // }),
        public GameBoardModel GetGameBoard(int index)
        {
            var boards = Boards;
            return boards[index % boards.Count];
        }
    }
}
