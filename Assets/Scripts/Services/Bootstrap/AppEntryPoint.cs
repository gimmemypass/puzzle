using Cysharp.Threading.Tasks;
using Zenject;

namespace Services.Bootstrap
{
    public class AppEntryPoint : ILateDisposable
    {
        private readonly string _entrySceneName;
        private readonly ZenjectSceneLoader _sceneLoader;
        private readonly SignalBus _signalBus;

        [Inject]
        public AppEntryPoint(
            string entrySceneName,
            ZenjectSceneLoader sceneLoader,
            SignalBus signalBus
        )
        {
            _entrySceneName = entrySceneName;
            _sceneLoader = sceneLoader;
        
            _signalBus = signalBus;
            _signalBus.Subscribe<AppBootstrappedSignal>(AppBootstrappedHandler);
        }

        public void LateDispose()
        {
            _signalBus.Unsubscribe<AppBootstrappedSignal>(AppBootstrappedHandler);
        }

        private async void AppBootstrappedHandler()
        {
            await _sceneLoader.LoadSceneAsync(_entrySceneName).ToUniTask();
        }
    }
}