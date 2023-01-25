using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Player
    {
        public int x;
        public int y;
        public char sprite;
        public Map map;

        public Player(int x, int y, char sprite, Map map)
        {
            this.x = x;
            this.y = y;
            this.sprite = sprite;
            this.map = map;
            Console.SetCursorPosition(this.x, this.y);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(sprite);
        }

        public void Move(ConsoleKey key)
        {
            if (x < Console.WindowWidth && x > 0 && y < Console.WindowHeight && y > 0)
            {
                map.DrawTile(x, y);
            }

            if((key == ConsoleKey.W || key == ConsoleKey.UpArrow) && map.CheckTile(x, y - 1))
            {
                y--;
            }
            if ((key == ConsoleKey.S || key == ConsoleKey.DownArrow) && map.CheckTile(x, y + 1))
            {
                y++;
            }
            if ((key == ConsoleKey.A || key == ConsoleKey.LeftArrow) && map.CheckTile(x - 1, y))
            {
                x--;
            }
            if ((key == ConsoleKey.D || key == ConsoleKey.RightArrow) && map.CheckTile(x + 1, y))
            {
                x++;
            }

            if (x < Console.WindowWidth && x > 0 && y < Console.WindowHeight && y > 0)
            {
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write(sprite);
            }
        }


    }
}
