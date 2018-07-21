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

        private List<MapTile> MapTileList { get; set; }

        public List<string> ErrorList { get; set; }

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

            MapTileList = new List<MapTile>();
            for (int currentRow = 0; currentRow < MapHeight; ++currentRow)
            {
                GenerateMapTileRow(currentRow);
            }

            CountSurroundingMinesAndSetCountForMapTiles();

            map.MapTiles = MapTileList.ToArray();
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

        private void GenerateMapTileRow(int currentRow)
        {
            for (int currentColumn = 0; currentColumn < MapWidth; ++currentColumn)
            {
                MapTileList.Add(GenerateMapTile(currentRow, currentColumn));
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
            return ((rand.Next(0, 100) + 0.0 + rand.NextDouble()) <= MAP_MINEPERCENTAGE_MIN)
                ? MapTileValueEnums.MINE
                : MapTileValueEnums.EMPTY;
        }

        private void CountSurroundingMinesAndSetCountForMapTiles()
        {
            foreach (MapTile mapTile in MapTileList)
            {
                mapTile.SurroundingMines = CountSurroundingMines(mapTile);
            }
        }

        private int CountSurroundingMines(MapTile mapTile)
        {
            int surroundingMines = 0;
            foreach (MapTile surroundingMapTile in MapTileList)
            {
                if (surroundingMapTile.IsMineTile())
                {
                    if (IsSurroundingMapTile(mapTile, surroundingMapTile))
                    {
                        ++surroundingMines;
                    }
                }
            }
            return surroundingMines;
        }

        private bool IsSurroundingMapTile(MapTile mapTile, MapTile surroundingMapTile)
        {
            return MapTileIsDiagonalSurrounding(mapTile, surroundingMapTile) || MapTileIsCrossSurrounding(mapTile, surroundingMapTile);
        }

        private bool MapTileIsDiagonalSurrounding(MapTile mapTile, MapTile surroundingMapTile)
        {
            return (surroundingMapTile.XPos == mapTile.XPos + 1 || surroundingMapTile.XPos == mapTile.XPos - 1) &&
                   (surroundingMapTile.YPos == mapTile.YPos + 1 || surroundingMapTile.YPos == mapTile.YPos - 1);
        }

        private bool MapTileIsCrossSurrounding(MapTile mapTile, MapTile surroundingMapTile)
        {
            return surroundingMapTile.XPos == mapTile.XPos && (surroundingMapTile.YPos == mapTile.YPos + 1 || surroundingMapTile.YPos == mapTile.YPos - 1) ||
                   surroundingMapTile.YPos == mapTile.YPos && (surroundingMapTile.XPos == mapTile.XPos + 1 || surroundingMapTile.XPos == mapTile.XPos - 1);
        }
    }
}
