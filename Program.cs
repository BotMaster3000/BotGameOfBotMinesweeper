using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotGameOfBotMinesweeper.MineSweeperDisplay;

namespace BotGameOfBotMinesweeper
{
    class Program
    {
        static void Main(string[] args)
        {
            ConsoleMineSweeper mineSweeper = new ConsoleMineSweeper();
            mineSweeper.SetupGame(50, 50, 10);
            mineSweeper.DisplayMap();
            Console.ReadLine();
        }
    }
}
