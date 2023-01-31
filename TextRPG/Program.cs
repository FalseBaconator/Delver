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

        /*static List<Enemy> enemies = new List<Enemy>
        {
            //new Enemy(10, 10, 1, 1, 'O', map, ConsoleColor.Red, player, enemyManager),
            //new Enemy(17, 3, 1, 1, 'O', map, ConsoleColor.Red, player, enemyManager),
            //new Enemy(31, 31, 1, 1, 'O', map, ConsoleColor.Red, player, enemyManager)

            *//*new Goblin(10, 10, map, player, enemyManager),
            new Goblin(17, 4, map, player, enemyManager),
            new Goblin(31, 31, map, player, enemyManager),
            new Slime(10, 31, map, player, enemyManager),
            new Slime(17, 31, map, player, enemyManager),
            new Slime(31, 10, map, player, enemyManager),*//*

        };*/

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            enemyManager.GenerateEnemies();
            //map.DrawMap();
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
