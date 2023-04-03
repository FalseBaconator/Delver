using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class MiniMap
    {
        private Tile[,] map = new Tile[Constants.mapHeight, Constants.mapWidth];

        private Player player;

        private int x;
        private int y;

        //public char[,] revealedMap = new char[Constants.mapHeight, Constants.mapWidth];

        //public ConsoleColor[,] foregroundColors = new ConsoleColor[Constants.mapHeight, Constants.mapWidth];
        //public ConsoleColor[,] backgroundColors = new ConsoleColor[Constants.mapHeight, Constants.mapWidth];

        public Tile[,] revealedMap = new Tile[Constants.mapHeight, Constants.mapWidth];

        public MiniMap(Tile[,] map, Player player)
        {
            for (int i = 0; i < revealedMap.GetLength(0); i++)
            {
                for (int j = 0; j < revealedMap.GetLength(1); j++)
                {
                    revealedMap[i, j] = new Tile(' ', Constants.borderColor, Constants.BGColor);
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
                    revealedMap[i, j].foregroundColor = ConsoleColor.White;
                    revealedMap[i, j].backgroundColor = ConsoleColor.Black;
                }
            }
        }

        public void Update()
        {
            resetColors();
            y = player.GetPos().x / Constants.roomWidth;
            x = player.GetPos().y / Constants.roomHeight;

            if (revealedMap[x, y].sprite == ' ')
                revealedMap[x, y] = map[x, y];
            revealedMap[x, y].foregroundColor = ConsoleColor.DarkYellow;

        }

        public void Refresh(Tile[,] map)
        {
            for (int i = 0; i < revealedMap.GetLength(0); i++)
            {
                for (int j = 0; j < revealedMap.GetLength(1); j++)
                {
                    revealedMap[i, j] = new Tile(' ', Constants.borderColor, Constants.BGColor);
                }
            }
        }

    }
}
