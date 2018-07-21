using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotGameOfBotMinesweeper.MineSweeperLogic;
using BotGameOfBotMinesweeper.MineSweeperModels;

namespace BotGameOfBotMinesweeper.MineSweeperDisplay
{
    public class ConsoleMineSweeper
    {
        private GameLogic gameLogic;

        private readonly char MINETILE_DISPLAYCHAR = 'M';
        private readonly char UNDISCOVERED_DISPLAYCHAR = 'X';

        public ConsoleMineSweeper()
        {

        }

        public void SetupGame(int mapHeight, int mapWidth, double minePercentage)
        {
            SetupGameLogic(mapHeight, mapWidth, minePercentage);
            SetupConsoleWindow();
        }

        public void DisplayMap()
        {
            for(int currentYPos = 0; currentYPos < gameLogic.Map.Height; ++currentYPos)
            {
                for(int currentXPos = 0; currentXPos < gameLogic.Map.Width; ++currentXPos)
                {
                    DisplayCurrentTile(currentYPos, currentXPos);
                }
            }
        }

        private void SetupGameLogic(int mapHeight, int mapWidth, double minePercentage)
        {
            Map map = CreateMap(mapHeight, mapWidth, minePercentage);
            if (map != null)
            {
                gameLogic = new GameLogic(map);
            }
        }

        private void SetupConsoleWindow()
        {
            if(gameLogic?.Map != null)
            {
                Console.WindowHeight = gameLogic.Map.Height;
                Console.WindowWidth = gameLogic.Map.Width;
            }
        }

        private Map CreateMap(int mapHeight, int mapWidth, double minePercentage)
        {
            MapCreator mapCreator = new MapCreator(mapHeight, mapWidth, minePercentage);
            return mapCreator.GenerateAndGetMap();
        }

        private void DisplayCurrentTile(int yPos, int xPos)
        {
            SetCursorPosition(yPos, xPos);
            WriteTileInformation(yPos, xPos);
        }

        private void SetCursorPosition(int yPos, int xPos)
        {
            Console.CursorTop = yPos;
            Console.CursorLeft = xPos;
        }

        private void WriteTileInformation(int yPos, int xPos)
        {
            MapTile mapTile = gameLogic.GetMapTile(yPos, xPos);
            if (mapTile.IsVisible)
            {
                if (mapTile.IsMineTile())
                {
                    Console.Write(MINETILE_DISPLAYCHAR);
                }
                else
                {
                    Console.Write(mapTile.SurroundingMines);
                }
            }
            else
            {
                Console.Write(UNDISCOVERED_DISPLAYCHAR);
            }
        }
    }
}
