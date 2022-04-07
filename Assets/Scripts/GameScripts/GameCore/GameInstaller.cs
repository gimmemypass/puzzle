using GameScripts.Configs;
using Zenject;

namespace GameScripts.GameCore
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameBoardProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<LevelSelectorService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerDataService>().AsSingle();

            Container.DeclareSignal<LevelCompletedSignal>();
        }
    }
}
