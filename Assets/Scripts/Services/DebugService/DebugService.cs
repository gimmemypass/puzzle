using System.Linq;
using Configs;
using GameScripts.GameCore;
using UnityEngine;
using Zenject;

namespace Services.DebugService
{
    public class DebugService : IInitializable 
    {
        private readonly GameConfig _gameConfig;
        private readonly ILevelLoader _levelLoader;
        private readonly IGameBoardProvider _gameBoardProvider;
        private int _level;

        [Inject]
        public DebugService(
            GameConfig gameConfig,
            ILevelLoader levelLoader,
            IGameBoardProvider gameBoardProvider
        )
        {
            _gameConfig = gameConfig;
            _levelLoader = levelLoader;
            _gameBoardProvider = gameBoardProvider;
        }
        
        public void Initialize()
        {
            SROptions.Current.DebugService = this;
        }

        public int Level
        {
            get => _level;
            set => _level = value;
        }

        public void LoadLevel()
        {
            _levelLoader.Unload();
            _levelLoader.LoadLevel(_gameBoardProvider.GetGameBoard(Level));
        }

        public float SwipeDuration
        {
            get => _gameConfig.SwipeDuration;
            set => _gameConfig.SwipeDuration = value;
        }

        public float FillingDelay
        {
            get => _gameConfig.FillingDelay;
            set => _gameConfig.FillingDelay = value;
        }

        public bool EnableCameraAtEnding
        {
            get => _gameConfig.EnableCameraAtEnding;
            set => _gameConfig.EnableCameraAtEnding = value;
        }

        public string TileColor
        {
            get => ColorUtility.ToHtmlStringRGB(_gameConfig.TileColor);
            set
            {
                if(ColorUtility.TryParseHtmlString($"#{value}", out var color))
                    _gameConfig.TileColor = color;
            }
        }

        public string WallTopColor
        {
             get => ColorUtility.ToHtmlStringRGB(_gameConfig.WallTopColor);
             set
             {
                 if(ColorUtility.TryParseHtmlString($"#{value}", out var color))
                     _gameConfig.WallTopColor = color;
             }           
        }
        public string WallDownColor
        {
             get => ColorUtility.ToHtmlStringRGB(_gameConfig.WallDownColor);
             set
             {
                 if(ColorUtility.TryParseHtmlString($"#{value}", out var color))
                     _gameConfig.WallDownColor = color;
             }           
        }

        public bool EnableParticles
        {
            get => _gameConfig.EnableParticles;
            set => _gameConfig.EnableParticles = value;
        }
    }
}