using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BotGameOfBotMinesweeper.MineSweeperModels
{
    public class MapTile
    {
        public int XPos { get; set; }
        public int YPos { get; set; }
        public MapTileValueEnums TileValue { get; set; }
        public int SurroundingMines { get; set; }
    }
}
