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

        static MapGenerator mapGen = new MapGenerator();
        static Map map = new Map(mapGen.RandomizeMap());

        static EnemyManager enemyManager = new EnemyManager(map);

        static Player player = new Player(17, 17, 5, 2, '@', map, enemyManager, ConsoleColor.White);

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            enemyManager.GenerateEnemies(5);
            while (play && player.alive)
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
                    enemyManager.MoveEnemies();
                }
            }

        }
    }
}
