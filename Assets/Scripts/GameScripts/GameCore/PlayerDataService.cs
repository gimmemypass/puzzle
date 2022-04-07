using UnityEngine;

namespace GameScripts.GameCore
{
    public class PlayerDataService
    {
        private const string CurrentLevelKey = "currentLevel";

        public int GetCurrentLevelNumber() => PlayerPrefs.GetInt(CurrentLevelKey);
        public void IncrementCurrentLevelNumber() => PlayerPrefs.SetInt(CurrentLevelKey, GetCurrentLevelNumber() + 1);
    }
}