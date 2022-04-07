using System;
using GameScripts.GameCore;
using GameScripts.GridCells;
using Services.Input;
using Services.Input.Data;
using UnityEngine;
using Utils;
using Zenject;
using Vector2 = UnityEngine.Vector2;

namespace GameScripts.Input
{
    public class TileSwipeInputSystem : IInitializable, IDisposable, ITileAnnouncer
    {
        private const float SwipeThreshold = 5f;

        private readonly IInputService _inputService;
        private readonly Camera _camera;

        private TileView _selectedTile;
        
        private Vector2 _touchDownPosition;
        private Vector2 _touchUpPosition;

        private Vector2 TouchDownPosition
        {
            get => _touchDownPosition;
            set => _touchDownPosition = value.Rotate(-45);
        }

        private Vector2 TouchUpPosition
        {
            get => _touchUpPosition;
            set => _touchUpPosition = value.Rotate(-45);
        }
        public event Action<Tile, Vector2> OnTileMoveRequested = delegate { };

        public TileSwipeInputSystem(
            IInputService inputService
            )
        {
            _inputService = inputService;
            _camera = Camera.main;
        }

        public void Initialize()
        {
            _inputService.AddPointerDownListener(HandlePointerDown);
            _inputService.AddPointerMoveListener(HandlePointerMove);
            _inputService.AddPointerUpListener(HandlePointerUp);
        }
        public void Dispose()
        {
            _inputService.RemovePointerDownListener(HandlePointerDown);
            _inputService.RemovePointerMoveListener(HandlePointerMove);
            _inputService.RemovePointerUpListener(HandlePointerUp);
        }

        private void HandlePointerUp(InputPointer pointer)
        {
            if (_selectedTile == null) return;
            
            TouchUpPosition = pointer.Position;
            DetectSwipe();
        }

        private void HandlePointerMove(InputPointer pointer)
        {
            if (_selectedTile == null) return;
            
            TouchUpPosition = pointer.Position;
            DetectSwipe();
        }

        private void HandlePointerDown(InputPointer pointer)
        {
            CheckForTile(pointer, out _selectedTile);
            
            TouchDownPosition = pointer.Position;
            TouchUpPosition = pointer.Position;
            
        }
        private bool CheckForTile(InputPointer pointer, out TileView tileView)
        {
            if (Physics.Raycast(GetScreenPosRay(pointer.Position), out RaycastHit hit))
            {
                if (hit.collider.gameObject.TryGetComponent<TileView>(out var tile))
                {
                    tileView = tile;
                    return true;
                }
            }
            tileView = default;
            return false;
        }

        private void DetectSwipe ()
        {
            if (VerticalMoveValue () > SwipeThreshold && VerticalMoveValue () > HorizontalMoveValue ()) {
                Debug.Log ("Vertical Swipe Detected!");
                if (TouchDownPosition.y - TouchUpPosition.y > 0) {
                    OnSwipe(new Vector2(0,1));
                } else if (TouchDownPosition.y - TouchUpPosition.y < 0) {
                    OnSwipe(new Vector2(0,-1));
                }
                TouchUpPosition = TouchDownPosition;

            } else if (HorizontalMoveValue () > SwipeThreshold && HorizontalMoveValue () > VerticalMoveValue ()) {
                Debug.Log ("Horizontal Swipe Detected!");
                if (TouchDownPosition.x - TouchUpPosition.x > 0) {
                    OnSwipe(new Vector2(-1,0));
                } else if (TouchDownPosition.x - TouchUpPosition.x < 0) {
                    OnSwipe(new Vector2(1,0));
                }
                TouchUpPosition = TouchDownPosition;
            } 
        }

        private void OnSwipe(Vector2 direction)
        {
            if (_selectedTile == null) return;
            
            OnTileMoveRequested?.Invoke(_selectedTile.Tile, direction);
            _selectedTile = null;
        }

        private float HorizontalMoveValue()
        {
            return Mathf.Abs(TouchUpPosition.x - TouchDownPosition.x);
        }

        private float VerticalMoveValue()
        {
            return Mathf.Abs(TouchUpPosition.y - TouchDownPosition.y);
        }

        private Ray GetScreenPosRay(Vector2 screenPos)
        {
            return _camera.ScreenPointToRay(screenPos);
        }
    }
}
