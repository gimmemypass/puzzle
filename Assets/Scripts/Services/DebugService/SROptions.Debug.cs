using System.ComponentModel;
using Services.DebugService;
using UnityEngine;

public partial class SROptions
{
    public DebugService DebugService { get; set; }

    [Category("Levels")]
    public int Level
    {
        get => DebugService.Level;
        set => DebugService.Level = value;
    }
    [Category("Levels")]
    public void LoadLevel() => DebugService.LoadLevel();

    public float SwipeDuration
    {
        get => DebugService.SwipeDuration;
        set => DebugService.SwipeDuration = value;
    }

    public float FillingDelay
    {
        get => DebugService.FillingDelay;
        set => DebugService.FillingDelay = value;
    }
    public bool EnableCameraAtEnding
    {
        get => DebugService.EnableCameraAtEnding;
        set => DebugService.EnableCameraAtEnding = value;
    }

    public string TileColor
    {
        get => DebugService.TileColor;
        set => DebugService.TileColor = value;
    }
    public string WallTopColor
    {
        get => DebugService.WallTopColor;
        set => DebugService.WallTopColor = value;
    }
    public string WallDownColor
    {
        get => DebugService.WallDownColor;
        set => DebugService.WallDownColor = value;
    }

    public bool EnableParticles
    {
        get => DebugService.EnableParticles;
        set => DebugService.EnableParticles = value;
    }
}
