using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Map
    {
        private Render rend;

        private char[,] map;

        public Map(char[,] grid, Render rend)
        {
            map = grid;
            this.rend = rend;
        }

        public void DrawMap()   //sets rend arrays to map chars
        {
            for (int i = 0; i < map.GetLength(1); i++)
            {
                for (int j = 0; j < map.GetLength(0); j++)
                {

                    rend.WholeMap[j, i] = new Tile(map[j, i], Constants.mapColor, Constants.BGColor);
                }
            }
        }

        public void NewMap(char[,] grid)
        {
            map = grid;
        }

        public bool isFloorAt(Position pos) //returns true if the provided coords is a floor
        {
            bool isFloor = false;
            if (map[pos.y,pos.x] == ',') isFloor = true;
            return isFloor;
        }


    }
}
