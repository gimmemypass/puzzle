using Services.Assets;
using Services.Bootstrap;
using Services.DebugService;
using Services.Input;
using Zenject;

namespace Services
{
    public class ServicesInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            SignalBusInstaller.Install(Container);
            BootstrapServiceInstaller.Install(Container);
            
            AssetsServiceInstaller.Install(Container);
            InputServiceInstaller.Install(Container);
            DebugServiceInstaller.Install(Container);
        }
    }
}