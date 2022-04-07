using Configs;
using UnityEngine;
using Zenject;

public class BackgroundController : MonoBehaviour
{
    [SerializeField] private Sprite _backSprite;
    [SerializeField] private Sprite _greenSprite;
    [SerializeField] private Background _background;
    [SerializeField] private ParticleSystem _particleSystem;
    
    private GameConfig _gameConfig;

    [Inject]
    private void Construct(GameConfig gameConfig)
    {
        _gameConfig = gameConfig;
    }

    private void Start()
    {
        _background.SetSprite(_backSprite);
        _particleSystem.gameObject.SetActive(_gameConfig.EnableParticles);
    }
}