using UnityEngine;

[RequireComponent(typeof(SpriteRenderer), typeof(SpriteRendererFitter))]
public class Background : MonoBehaviour
{
    private SpriteRenderer _spriteRenderer;
    private SpriteRendererFitter _fitter;

    public void SetSprite(Sprite sprite)
    {
        _spriteRenderer.sprite = sprite;
        _fitter.FitScreen();
    }
    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _fitter = GetComponent<SpriteRendererFitter>();
    }
    
}