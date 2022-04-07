using Zenject;

namespace Services.Bootstrap
{
    public class BootstrapServiceInstaller : Installer<BootstrapServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.DeclareSignal<AppBootstrappedSignal>().OptionalSubscriber();
            Container.BindInterfacesAndSelfTo<BootstrapService>().AsSingle().NonLazy();
        }
    }
}