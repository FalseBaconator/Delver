using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Program
    {

        static bool play = true;
        static ConsoleKey key;

        static Map map = new Map();

        static Player player = new Player(14, 14, '@', map, ConsoleColor.White);



        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            //map.Draw();
            while (play)
            {
                if(Console.CursorVisible) Console.CursorVisible = false;
                key = Console.ReadKey(true).Key;
                if(key == ConsoleKey.Escape)
                {
                    play = false;
                }
                else
                {
                    player.Move(key);
                }
            }

        }
    }
}
