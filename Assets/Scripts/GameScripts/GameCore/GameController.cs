using System;
using Configs;
using Cysharp.Threading.Tasks;
using GameScripts.GridCells;
using GameScripts.Input;
using UnityEngine;
using Zenject;

namespace GameScripts.GameCore
{
    public class GameController : IInitializable, IDisposable
    {
        private readonly ITileAnnouncer _tileAnnouncer;
        private readonly GameBoard3DView _gameBoard3DView;
        private readonly CameraController _cameraController;
        private readonly SignalBus _signalBus;
        private readonly GameConfig _gameConfig;
        private readonly GameBoardController _boardController;

        public GameController(
            ITileAnnouncer tileAnnouncer,
            GameBoard3DView gameBoard3DView,
            CameraController cameraController,
            SignalBus signalBus,
            GameConfig gameConfig,
            GameBoardController boardController
            )
        {
            _tileAnnouncer = tileAnnouncer;
            _gameBoard3DView = gameBoard3DView;
            _cameraController = cameraController;
            _signalBus = signalBus;
            _gameConfig = gameConfig;
            _boardController = boardController;
        }

        public void Initialize()
        {
            _gameBoard3DView.InstantiateBoard(_boardController.Grid);
            Debug.Log(_boardController.ToString());
            _tileAnnouncer.OnTileMoveRequested += AttemptMove;
        }

        public void Dispose()
        {
            _tileAnnouncer.OnTileMoveRequested -= AttemptMove;
        }

        public async void AttemptMove(Tile tile, Vector2 moveDirection)
        {
            bool hasMoved;
            hasMoved = _boardController.TryMove(tile, moveDirection, out Vector2Int newPos);
            if (hasMoved)
            {
                _gameBoard3DView.MoveCell(tile, newPos);
            }

            if (_boardController.IsGameEnded())
            {
                Debug.Log("GameEnded");
                _tileAnnouncer.OnTileMoveRequested -= AttemptMove; 
                await EndGame();
            }
            Debug.Log(_boardController.ToString());
        }

        private async UniTask EndGame()
        {
            _boardController.EndGame();
            await UniTask.Delay((int)(_gameConfig.FillingDelay * 1000));
            var tiles = _boardController.GetCells<Tile>();
            foreach (var tile in tiles)
            {
                foreach (var tileDirection in tile.TileDirections)
                {
                    foreach (var fillTile in tileDirection.GetFillTiles())
                    {
                        int row, column;
                        (row, column) = _boardController.Grid.CoordinatesOf(fillTile);
                        _gameBoard3DView.CreateFillTile(row, column, fillTile, tileDirection.Color);
                    }
                }
                await UniTask.WhenAll(tile.TileDirections.Select(d => _gameBoard3DView.FillTileDirection(d, tile)));
            }
            if(_gameConfig.EnableCameraAtEnding)
                _cameraController.SwitchCamera();
            await UniTask.Delay(2000);
            _signalBus.Fire<LevelCompletedSignal>();
        }
    }
}