using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[ExecuteAlways]
public class SpriteRendererFitter : MonoBehaviour
{
    [SerializeField] private Camera _camera;
    private SpriteRenderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        FitScreen();
    }

    public void FitScreen()
    {
        transform.localScale = new Vector3(1, 1, 1);
        var width = _renderer.sprite.bounds.size.x;
        var heigth = _renderer.sprite.bounds.size.y;

        var worldScreenHeight = _camera.orthographicSize * 2.0f;
        var worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        transform.localScale = new Vector3(worldScreenWidth / width, worldScreenHeight / heigth, 1);
    }
}