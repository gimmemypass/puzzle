using System;
using GameScripts.GridCells;
using UnityEngine;

namespace GameScripts.Input
{
    public interface ITileAnnouncer
    {
        event Action<Tile, Vector2> OnTileMoveRequested;
    }
}