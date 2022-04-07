using Zenject;

namespace Services.DebugService
{
    public class DebugServiceInstaller : Installer<DebugServiceInstaller>
    {
        public override void InstallBindings()
        {
            if (UnityEngine.Debug.isDebugBuild)
                Container.BindInterfacesAndSelfTo<DebugService>().AsSingle();
        }
    }
}