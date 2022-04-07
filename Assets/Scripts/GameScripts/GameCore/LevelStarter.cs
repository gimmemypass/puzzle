using System;
using UnityEngine;
using Zenject;

namespace GameScripts.GameCore
{
    public class LevelStarter : MonoBehaviour
    {
        private ILevelLoader _levelLoader;
        private LevelSelectorService _levelSelectorService;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(
            ILevelLoader levelLoader,
            SignalBus signalBus,
            LevelSelectorService levelSelectorService
        )
        {
            _signalBus = signalBus;
            _levelSelectorService = levelSelectorService;
            _levelLoader = levelLoader;
        }

        private void OnEnable()
        {
            _signalBus.Subscribe<LevelCompletedSignal>(HandleLevelCompletion);
        }

        private void OnDisable()
        {
            _signalBus.Unsubscribe<LevelCompletedSignal>(HandleLevelCompletion);
        }

        private void HandleLevelCompletion(LevelCompletedSignal signal)
        {
            _levelSelectorService.PrepareNextLevel();
            LoadCurrentLevel();
        }

        private void Start()
        {
            LoadCurrentLevel();
        }

        private void LoadCurrentLevel()
        {
            _levelLoader.Unload();
            _levelLoader.LoadLevel(_levelSelectorService.GetCurrentLevel());
        }
    }
}
