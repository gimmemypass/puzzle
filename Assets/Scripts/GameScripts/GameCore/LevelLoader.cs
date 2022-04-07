using Cysharp.Threading.Tasks;
using GameScripts.Configs;
using UnityEngine.SceneManagement;
using Zenject;

namespace GameScripts.GameCore
{
    public interface ILevelLoader
    {
        UniTask LoadLevel(GameBoardModel model);
        UniTask Unload();
    }

    public class LevelLoader : ILevelLoader
    {
        private const string LEVEL_SCENE_NAME = "LevelContext";
        
        private readonly ZenjectSceneLoader _sceneLoader;
        private readonly IGameBoardProvider _gameBoardProvider;

        [Inject]
        public LevelLoader(
            ZenjectSceneLoader sceneLoader,
            IGameBoardProvider gameBoardProvider
        )
        {
            _sceneLoader = sceneLoader;
            _gameBoardProvider = gameBoardProvider;
        }
        public async UniTask LoadLevel(GameBoardModel model)
        {
            await _sceneLoader.LoadSceneAsync(LEVEL_SCENE_NAME, LoadSceneMode.Additive, diContainer =>
            {
                diContainer.Bind<GameBoardModel>().FromInstance(model);
            });
        }

        public async UniTask Unload()
        {
            if(SceneManager.GetSceneByName(LEVEL_SCENE_NAME).isLoaded)
                await SceneManager.UnloadSceneAsync(LEVEL_SCENE_NAME);
        }
    }
}