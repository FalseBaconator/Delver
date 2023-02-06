using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Program
    {


        static Render render = new Render();

        static MapGenerator mapGen = new MapGenerator();
        static Map map = new Map(mapGen.RandomizeMap(), render);

        static EnemyManager enemyManager = new EnemyManager(map, render);

        static Player player = new Player(17, 17, 5, 2, '@', map, enemyManager, ConsoleColor.White, render);

        static InputManager inputManager = new InputManager(player);
        static GameManager manager = new GameManager(player, enemyManager, map, inputManager, render);
        
        static void Main(string[] args)
        {
            enemyManager.GenerateEnemies(5);
            manager.Draw();
            while (manager.play)
            {
                manager.Update();
                manager.Draw();
            }
            Console.Clear();
            Console.ResetColor();
            Console.Write("You are dead, no big surprise");
            Console.ReadKey(true);

        }
    }
}
