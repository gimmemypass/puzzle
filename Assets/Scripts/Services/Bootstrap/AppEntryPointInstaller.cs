using UnityEngine;
using Zenject;

namespace Services.Bootstrap
{
    public class AppEntryPointInstaller : MonoInstaller
    {
        [SerializeField] private string _entrySceneName;
    
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<AppEntryPoint>()
                .FromSubContainerResolve()
                .ByMethod(InstallEntryPoint)
                .AsSingle()
                .NonLazy();
        }

        private void InstallEntryPoint(DiContainer container)
        {
            container.BindInstance(_entrySceneName);
            container.BindInterfacesAndSelfTo<AppEntryPoint>().AsSingle();
        }
    }
}