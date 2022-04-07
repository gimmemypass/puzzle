using UnityEngine;

namespace Configs
{
    [CreateAssetMenu(menuName = "Configs/GameConfig", fileName = "GameConfig", order = 0)]
    public class GameConfig : ScriptableObject
    {
        public float SwipeDuration = 0.5f;
        public float FillingDelay = 1f;
        public bool EnableCameraAtEnding = true;
        public Color TileColor = new Color(58 / 255f, 191 / 255f, 1);
        public Color WallTopColor;
        public Color WallDownColor;
        public bool EnableParticles = true;
    }
}