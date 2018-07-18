using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BotGameOfBotMinesweeper.MineSweeperModels;

namespace BotGameOfBotMinesweeper.MineSweeperLogic
{
    public class MapCreator
    {
        private const int MAP_HEIGHT_MIN = 1;
        private const int MAP_WIDTH_MIN = 1;
        private const double MAP_MINEPERCENTAGE_MIN = 5.00;

        private int MapHeight { get; }
        private int MapWidth { get; }
        private double MinePercentage { get; }

        Random rand = new Random();

        public MapCreator(int mapHeight, int mapWidth, double minePercentage)
        {
            this.MapHeight = mapHeight;
            this.MapWidth = mapWidth;
            this.MinePercentage = minePercentage;
        }

        public Map GenerateAndGetMap()
        {
            Map map = null;
            if (ParametersAreInBounds())
            {
                map = GenerateMap();
            }
            return map;
        }

        private bool ParametersAreInBounds()
        {
            return MapHeight >= MAP_HEIGHT_MIN && MapWidth >= MAP_WIDTH_MIN && MinePercentage >= MAP_MINEPERCENTAGE_MIN;
        }

        private Map GenerateMap()
        {
            Map map = InitializeMineSweeperMap();

            List<MapTile> mapTiles = new List<MapTile>();
            for(int currentRow = 0; currentRow < MapHeight; ++currentRow)
            {
                GenerateMapTileRow(mapTiles, currentRow);
            }

            // TODO: DIE ANZAHL DER IN DER NÄHE BEFINDLICHEN MINEN MUSS NOCH GEZÄHLT WERDEN

            map.MapTiles = mapTiles.ToArray();
            return map;
        }

        private Map InitializeMineSweeperMap()
        {
            return new Map()
            {
                MapTiles = new MapTile[MapHeight * MapWidth],
                Height = MapHeight,
                Width = MapWidth,
            };
        }

        private void GenerateMapTileRow(List<MapTile> mapTiles, int currentRow)
        {
            for(int currentColumn = 0; currentColumn < MapWidth; ++currentColumn)
            {
                mapTiles.Add(GenerateMapTile(currentRow, currentColumn));
            }
        }

        private MapTile GenerateMapTile(int currentRow, int currentColumn)
        {
            return new MapTile()
            {
                YPos = currentRow,
                XPos = currentColumn,
                TileValue = DetermineMapTileValue()
            };
        }

        private MapTileValueEnums DetermineMapTileValue()
        {
            if((rand.Next(0, 100) + 0.0 + rand.NextDouble()) <= MAP_MINEPERCENTAGE_MIN)
            {
                return MapTileValueEnums.MINE;
            }
            else
            {
                return MapTileValueEnums.EMPTY;
            }
        }
    }
}
