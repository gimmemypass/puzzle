using Cinemachine;
using GameScripts.Input;
using UnityEngine;
using Zenject;

namespace GameScripts.GameCore
{
    public class LevelInstaller : MonoInstaller
    {
        [SerializeField]
        private GameBoard3DView gameBoard3DView;

        [SerializeField] private CinemachineVirtualCamera _mainCamera;
        [SerializeField] private CinemachineVirtualCamera _endingCamera;
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<TileSwipeInputSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameController>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<GameBoardController>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameBoard3DView>().FromInstance(gameBoard3DView);
            BindCameraSwitcher(Container);
        }

        private void BindCameraSwitcher(DiContainer container)
        {
            container.BindInstance(_mainCamera).WithId(CameraIds.MainCamera).WhenInjectedInto<CameraController>();
            container.BindInstance(_endingCamera).WithId(CameraIds.EndingCamera).WhenInjectedInto<CameraController>();
            container.BindInterfacesAndSelfTo<CameraController>().AsSingle();    
        }
    }
}