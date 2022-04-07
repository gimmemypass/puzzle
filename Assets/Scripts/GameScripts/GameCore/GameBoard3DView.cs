using System.Collections.Generic;
using Configs;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using GameScripts.GridCells;
using UnityEngine;
using Zenject;

namespace GameScripts.GameCore
{
    public class GameBoard3DView : MonoBehaviour, ICellVisitor
    {
        [SerializeField] private WallView _wallPrefab;
        [SerializeField] private TileView _tilePrefab;
        [SerializeField] private FillTileView _fillTilePrefab;

        private const int BoardHeight = 12;
        private const float CellSize = 1;

        private Vector2 _centerOffset;

        private int _row;
        private int _column;
        private Vector3 _boardCenter;
        private Dictionary<Cell, CellView> _cellViewMap;
        private Vector2 _widthLength;
        private GameConfig _gameConfig;

        [Inject]
        private void Construct(GameConfig gameConfig)
        {
            _gameConfig = gameConfig;
        }

        public void InstantiateBoard(Cell[,] board)
        {
            _cellViewMap = new Dictionary<Cell, CellView>();

            var rowCount = board.GetLength(0);
            var columnCount = board.GetLength(1);
            _widthLength = new Vector2(columnCount * CellSize, rowCount * CellSize);
            
            for (_row = 0; _row < rowCount; _row++)
            {
                for (_column = 0; _column < columnCount; _column++)
                {
                    board[_row, _column].Accept(this);
                }
            }

            _boardCenter = new Vector3(_widthLength.y / 2 - CellSize / 2, 0,_widthLength.x / 2 - CellSize / 2); 
            foreach (Transform child in GetComponentInChildren<Transform>())
            {
                child.position -= _boardCenter;
            }
            
            // FillBoard();
        }

        public async UniTask MoveCell(Cell cell, Vector2Int to)
        {
            var toPos = GetScenePosition(to);
            var cellView = _cellViewMap[cell];
            await cellView.transform.DOLocalMove(toPos, _gameConfig.SwipeDuration).SetEase(Ease.InOutQuint).ToUniTask();
        }

        public void CreateFillTile(int row, int column, FillTile fillTile, Color color)
        {
            if (_cellViewMap.ContainsKey(fillTile))
                return;
            
            var fillTileView = Instantiate(_fillTilePrefab);
            fillTileView.transform.parent = transform;
            fillTileView.transform.localPosition = new Vector3(row * CellSize, -(BoardHeight - 1) * CellSize/2, column * CellSize);
            fillTileView.transform.position -= _boardCenter;
            fillTileView.InitializeColor(color);
            fillTileView.FillTile = fillTile;
            _cellViewMap[fillTile] = fillTileView;
            fillTileView.gameObject.SetActive(false);
        }

        public async UniTask FillTileDirection(TileDirection tileDirection, Tile tile)
        {
            async UniTask LiftFillTile(FillTileView fillTileView)
            {
                fillTileView.gameObject.SetActive(true);
                var sequence = DOTween.Sequence();
                sequence
                    .Append(fillTileView.transform.DOMoveY(0f, 0.4f))
                    .Join(fillTileView.TweenColor(tileDirection.Color, 0.4f))
                    .Append(fillTileView.transform.DOMoveY(0.2f, 0.1f))
                    .Append(fillTileView.transform.DOMoveY(0, 0.1f))
                    ;
                await sequence;
            }

            var tileView = (TileView)_cellViewMap[tile];
            await tileView.DisableArrow(tileDirection);
            var delay = 100;
            var uniTasks = new List<UniTask>();
            foreach (var fillTile in tileDirection.GetFillTiles())
            {
                var fillTileView = (FillTileView)_cellViewMap[fillTile];
                await UniTask.Delay(delay);
                uniTasks.Add(LiftFillTile(fillTileView));
            }
            await UniTask.WhenAll(uniTasks);
        }


        void ICellVisitor.Visit(EmptyCell cell) { }

        void ICellVisitor.Visit(Tile tile) => CreateTileView(tile);

        void ICellVisitor.Visit(Wall wall) => CreateWallView();

        void ICellVisitor.Visit(FillTile fillTile) { }

        private void CreateWallView()
        {
            var wallView = Instantiate(_wallPrefab);
            wallView.transform.parent = transform;
            wallView.TopSharedMaterial.color = _gameConfig.WallTopColor;
            wallView.DownSharedMaterial.color = _gameConfig.WallDownColor;
            wallView.transform.localPosition = new Vector3(_row * CellSize, -(BoardHeight - 1) * CellSize / 2, _column * CellSize);
            wallView.transform.localScale = new Vector3(1, BoardHeight * CellSize, 1);
        }

        private void CreateTileView(Tile tile)
        {
            var tileView = Instantiate(_tilePrefab);
            tileView.Tile = tile;
            tileView.GetComponent<MeshRenderer>().material.color = tile.TileColor;
            tileView.transform.parent = transform;
            tileView.transform.localPosition = new Vector3(_row * CellSize, 0, _column * CellSize);
            foreach (var direction in tile.TileDirections)
            {
                tileView.AddDirection(direction);
            }

            _cellViewMap[tile] = tileView;
        }

        private Vector3 GetScenePosition(Vector2Int from)
        {
            var pos = new Vector3(from.x * CellSize, 0, from.y * CellSize);
            return pos - _boardCenter;
        }
    }
}