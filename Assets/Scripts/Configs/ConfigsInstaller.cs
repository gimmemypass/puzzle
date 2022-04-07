using UnityEngine;
using Zenject;

namespace Configs
{
    [CreateAssetMenu(fileName = "ConfigsInstaller", menuName = "Configs/Installer")]
    public class ConfigsInstaller : ScriptableObjectInstaller<ConfigsInstaller>
    {
        [SerializeField] private GameConfig _gameConfig;
    
        public override void InstallBindings()
        {
            Container.BindInstances(
                _gameConfig
            );
        }
    }
}