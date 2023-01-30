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

        static EnemyManager enemyManager = new EnemyManager();

        static Player player = new Player(14, 14, 5, 2, '@', map, enemyManager, ConsoleColor.White);

        static List<Enemy> enemies = new List<Enemy>
        {
            new Enemy(10, 10, 1, 1, 'O', map, ConsoleColor.Red, player, enemyManager),
            new Enemy(20, 11, 1, 1, 'O', map, ConsoleColor.Red, player, enemyManager),
            new Enemy(25, 25, 1, 1, 'O', map, ConsoleColor.Red, player, enemyManager)
        };

        static void Main(string[] args)
        {
            Console.CursorVisible = false;
            enemyManager.Enemies = enemies;
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
                    enemyManager.MoveEnemies();
                }
            }

        }
    }
}
