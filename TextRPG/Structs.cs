using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal struct Position
    {
        public int x;
        public int y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public static bool operator ==(Position a, Position b)
        {
            if (a.x == b.x && a.y == b.y) return true;
            else return false;
        }

        public static bool operator !=(Position a, Position b)
        {
            if (a.x != b.x || a.y != b.y) return true;
            else return false;
        }

    }

    internal struct Tile
    {
        public char sprite;
        public ConsoleColor foregroundColor;
        public ConsoleColor backgroundColor;

        public Tile(char sprite, ConsoleColor foregroundColor, ConsoleColor backgroundColor)
        {
            this.sprite = sprite;
            this.foregroundColor = foregroundColor;
            this.backgroundColor = backgroundColor;
        }

        public override string ToString()
        {
            Console.BackgroundColor = backgroundColor;
            Console.ForegroundColor = foregroundColor;
            return sprite.ToString();
        }

        public static bool operator ==(Tile a, Tile b)
        {
            if(a.sprite == b.sprite && a.foregroundColor == b.foregroundColor && a.backgroundColor == b.backgroundColor) return true;
            else return false;
        }

        public static bool operator !=(Tile a, Tile b)
        {
            if (a.sprite != b.sprite || a.foregroundColor != b.foregroundColor || a.backgroundColor != b.backgroundColor) return true;
            else return false;
        }

    }

}
