using System.Linq;
using GameScripts.Configs;

namespace GameScripts.GameCore
{
    public class LevelSelectorService
    {
        private readonly IGameBoardProvider _gameBoardProvider;
        private readonly PlayerDataService _playerDataService;

        public LevelSelectorService(
            IGameBoardProvider gameBoardProvider,
            PlayerDataService playerDataService
        )
        {
            _gameBoardProvider = gameBoardProvider;
            _playerDataService = playerDataService;
        }
        public GameBoardModel GetCurrentLevel()
        {
            var currentLevelNumber = _playerDataService.GetCurrentLevelNumber();
            GameBoardModel model = _gameBoardProvider.GetGameBoard(currentLevelNumber);
            return model;
        }

        public void PrepareNextLevel()
        {
            _playerDataService.IncrementCurrentLevelNumber(); 
        }
    }
}