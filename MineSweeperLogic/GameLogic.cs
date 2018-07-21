using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotGameOfBotMinesweeper.MineSweeperModels;

namespace BotGameOfBotMinesweeper.MineSweeperLogic
{
    public class GameLogic
    {
        public Map Map { get; }
        public List<string> ErrorLog { get; }

        public bool IsGameOver { get; private set; }

        public GameLogic(Map map)
        {
            Map = map;
            ErrorLog = new List<string>();
        }

        public void EnterCoordinate(int xPos, int yPos)
        {
            MapTile tile = GetMapTile(xPos, yPos);
            if(tile != null)
            {
                CheckForGameOver(tile);
            }
        }

        private MapTile GetMapTile(int xPos, int yPos)
        {
            if (IsValidCoordinate(xPos, yPos))
            {
                foreach (MapTile tile in Map.MapTiles)
                {
                    if (tile.XPos == xPos && tile.YPos == yPos)
                    {
                        return tile;
                    }
                }
            }
            return null;
        }

        private bool IsValidCoordinate(int xPos, int yPos)
        {
            return (xPos > 0 && xPos < Map.Width) &&
                   (yPos > 0 && yPos < Map.Height);
        }

        private void CheckForGameOver(MapTile tile)
        {
            if (tile.IsMineTile())
            {
                IsGameOver = true;
            }
        }
    }
}
