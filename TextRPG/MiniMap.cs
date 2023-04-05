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

        private int rx;
        private int ry;

        public Tile[,] revealedMap = new Tile[Constants.mapHeight * 3, Constants.mapWidth * 3];

        private bool[,] isRevealed = new bool[Constants.mapHeight, Constants.mapWidth];

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
            for (int i = 0; i < isRevealed.GetLength(0); i++)
            {
                for (int j = 0; j < isRevealed.GetLength(1); j++)
                {
                    isRevealed[i, j] = false;
                }
            }
        }

        public void reset()
        {
            for (int i = 0; i < revealedMap.GetLength(0); i++)
            {
                for (int j = 0; j < revealedMap.GetLength(1); j++)
                {
                    if (revealedMap[i, j].sprite == Constants.playerSprite.sprite)
                        revealedMap[i, j] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                }
            }
        }

        public void Update()
        {
            reset();
            ry = ((player.GetPos().x / Constants.roomWidth) * 3) + 1;
            rx = ((player.GetPos().y/ Constants.roomHeight) * 3) + 1;
            int x = player.GetPos().y / Constants.roomWidth;
            int y = player.GetPos().x / Constants.roomWidth;


            if (isRevealed[x,y] == false)
            {
                isRevealed[x, y] = true;
                switch(map[x,y].sprite){
                    case '^':
                        revealedMap[rx - 1, ry - 1] = new Tile('┌', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry + 1] = new Tile('┐', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx , ry - 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx , ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx , ry + 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry - 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry + 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        break;
                    case '>':
                        revealedMap[rx - 1, ry - 1] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry + 1] = new Tile('┐', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry - 1] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry + 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry - 1] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry + 1] = new Tile('┘', ConsoleColor.White, ConsoleColor.Black);
                        break;
                    case 'V':
                        revealedMap[rx - 1, ry - 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry + 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry - 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry + 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry - 1] = new Tile('└', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry + 1] = new Tile('┘', ConsoleColor.White, ConsoleColor.Black);
                        break;
                    case '<':
                        revealedMap[rx - 1, ry - 1] = new Tile('┌', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry + 1] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry - 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry + 1] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry - 1] = new Tile('└', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry + 1] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        break;
                    case '┐':
                        revealedMap[rx - 1, ry - 1] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry + 1] = new Tile('┐', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry - 1] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry + 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry - 1] = new Tile('┐', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry + 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        break;
                    case '┌':
                        revealedMap[rx - 1, ry - 1] = new Tile('┌', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry + 1] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry - 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry + 1] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry - 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry + 1] = new Tile('┌', ConsoleColor.White, ConsoleColor.Black);
                        break;
                    case '│':
                        revealedMap[rx - 1, ry - 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry + 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry - 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry + 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry - 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry + 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        break;
                    case '─':
                        revealedMap[rx - 1, ry - 1] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry + 1] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry - 1] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry + 1] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry - 1] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry + 1] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        break;
                    case '┘':
                        revealedMap[rx - 1, ry - 1] = new Tile('┘', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry + 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry - 1] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry + 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry - 1] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry + 1] = new Tile('┘', ConsoleColor.White, ConsoleColor.Black);
                        break;
                    case '└':
                        revealedMap[rx - 1, ry - 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry + 1] = new Tile('└', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry - 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry + 1] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry - 1] = new Tile('└', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry + 1] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        break;
                    case '┬':
                        revealedMap[rx - 1, ry - 1] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry + 1] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry - 1] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry + 1] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry - 1] = new Tile('┐', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry + 1] = new Tile('┌', ConsoleColor.White, ConsoleColor.Black);
                        break;
                    case '┤':
                        revealedMap[rx - 1, ry - 1] = new Tile('┘', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry + 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry - 1] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry + 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry - 1] = new Tile('┐', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry + 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        break;
                    case '├':
                        revealedMap[rx - 1, ry - 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry + 1] = new Tile('└', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry - 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry + 1] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry - 1] = new Tile('│', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry + 1] = new Tile('┌', ConsoleColor.White, ConsoleColor.Black);
                        break;
                    case '┴':
                        revealedMap[rx - 1, ry - 1] = new Tile('┘', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry + 1] = new Tile('└', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry - 1] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry + 1] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry - 1] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry + 1] = new Tile('─', ConsoleColor.White, ConsoleColor.Black);
                        break;
                    case '┼':
                        revealedMap[rx - 1, ry - 1] = new Tile('┘', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx - 1, ry + 1] = new Tile('└', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry - 1] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx, ry + 1] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry - 1] = new Tile('┐', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                        revealedMap[rx + 1, ry + 1] = new Tile('┌', ConsoleColor.White, ConsoleColor.Black);
                        break;
                }
            }
            revealedMap[rx, ry] = new Tile(Constants.playerSprite.sprite, ConsoleColor.White, ConsoleColor.Black);

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
            for (int i = 0; i < isRevealed.GetLength(0); i++)
            {
                for (int j = 0; j < isRevealed.GetLength(1); j++)
                {
                    isRevealed[i, j] = false;
                }
            }
        }

    }
}
