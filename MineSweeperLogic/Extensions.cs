using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotGameOfBotMinesweeper.MineSweeperModels;

namespace BotGameOfBotMinesweeper.MineSweeperLogic
{
    public static class Extensions
    {
        public static bool IsMineTile(this MapTile mapTile)
        {
            return mapTile.TileValue == MapTileValueEnums.MINE;
        }
    }
}
