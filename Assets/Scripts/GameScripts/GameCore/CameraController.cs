using Cinemachine;
using GameScripts.Input;
using UnityEngine;
using Zenject;

namespace GameScripts.GameCore
{
    public class CameraController: IInitializable
    {
        private readonly GameBoard3DView _gameBoard3DView;
        private readonly CinemachineVirtualCamera _endingCamera;
        private readonly CinemachineVirtualCamera _mainCamera;

        [Inject]
        public CameraController(
            GameBoard3DView gameBoard3DView,
            [Inject(Id = CameraIds.EndingCamera)] CinemachineVirtualCamera endingCamera,
            [Inject(Id = CameraIds.MainCamera)] CinemachineVirtualCamera mainCamera
        )
        {
            _gameBoard3DView = gameBoard3DView;
            _endingCamera = endingCamera;
            _mainCamera = mainCamera;
        }

        public void SwitchCamera()
        {
            if (_mainCamera.Priority >= _endingCamera.Priority)
            {
                _mainCamera.Priority = 0;
                _endingCamera.Priority = 10;
            }
            else
            {
                _endingCamera.Priority = 0;               
                _mainCamera.Priority = 10;
            }
        }

        void IInitializable.Initialize()
        {
            _endingCamera.transform.position += _gameBoard3DView.transform.position;
            _endingCamera.Priority = 0;               
            _mainCamera.Priority = 10;           
        }
    }
}