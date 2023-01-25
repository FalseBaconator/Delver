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

        public Player(int x, int y, char sprite)
        {
            this.x = x;
            this.y = y;
            this.sprite = sprite;
            Console.SetCursorPosition(this.x, this.y);
            Console.Write(sprite);
        }

        public void Move(ConsoleKey key)
        {
            Console.SetCursorPosition(x, y);
            Console.Write(' ');
            if(key == ConsoleKey.W || key == ConsoleKey.UpArrow)
            {
                y--;
            }
            if (key == ConsoleKey.S || key == ConsoleKey.DownArrow)
            {
                y++;
            }
            if (key == ConsoleKey.A || key == ConsoleKey.LeftArrow)
            {
                x--;
            }
            if (key == ConsoleKey.D || key == ConsoleKey.RightArrow)
            {
                x++;
            }
            Console.SetCursorPosition(x, y);
            Console.Write(sprite);
        }


    }
}
