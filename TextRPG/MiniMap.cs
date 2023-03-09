using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class MiniMap
    {
        private char[,] map = new char[Constants.mapHeight, Constants.mapWidth];

        private Player player;

        private int x;
        private int y;

        public char[,] revealedMap = new char[Constants.mapHeight, Constants.mapWidth];

        public ConsoleColor[,] foregroundColors = new ConsoleColor[Constants.mapHeight, Constants.mapWidth];
        public ConsoleColor[,] backgroundColors = new ConsoleColor[Constants.mapHeight, Constants.mapWidth];

        public MiniMap(char[,] map, Player player)
        {
            for (int i = 0; i < revealedMap.GetLength(0); i++)
            {
                for (int j = 0; j < revealedMap.GetLength(1); j++)
                {
                    revealedMap[i, j] = ' ';
                }
            }
            this.map = map;
            this.player = player;
        }

        public void resetColors()
        {
            for (int i = 0; i < Constants.mapHeight; i++)
            {
                for (int j = 0; j < Constants.mapWidth; j++)
                {
                    foregroundColors[i, j] = ConsoleColor.White;
                    backgroundColors[i, j] = ConsoleColor.Black;
                }
            }
        }

        public void Update()
        {
            resetColors();
            y = player.GetX() / Constants.roomWidth;
            x = player.GetY() / Constants.roomHeight;

            if (revealedMap[x, y] == ' ')
                revealedMap[x, y] = map[x, y];
            backgroundColors[x, y] = ConsoleColor.DarkYellow;
            foregroundColors[x, y] = ConsoleColor.Black;

        }


    }
}
