using System.Collections.Generic;
using GameScripts.Configs;

namespace GameScripts.GameCore
{
    public interface IGameBoardProvider
    {
        GameBoardModel GetGameBoard(int index);
    }
}