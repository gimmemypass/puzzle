using Zenject;

namespace Services.Assets
{
    public class AssetsServiceInstaller : Installer<AssetsServiceInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AssetsService>().AsSingle();
        }
    }
}