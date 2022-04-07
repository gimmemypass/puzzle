using UnityEngine;
using Zenject;

namespace Services.Bootstrap
{
    public class BootstrapService : IInitializable
    {
        private SignalBus _signalBus;

        [Inject]
        public BootstrapService(
            SignalBus signalBus
        )
        {
            _signalBus = signalBus;
        }
        public void Initialize()
        {
            Bootstrap();
        }

        private void Bootstrap()
        {
            Application.targetFrameRate = 60;
            _signalBus.Fire<AppBootstrappedSignal>();
        }
    }
}