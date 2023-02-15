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

        static Player player = new Player(17, 17, 5, 5, 2, '@', map, enemyManager, ConsoleColor.White, render);

        static ItemManager itemManager = new ItemManager(player, enemyManager, map, render);

        static InputManager inputManager = new InputManager(player);
        static GameManager manager = new GameManager(player, enemyManager, map, inputManager, itemManager, render);
        
        static void Main(string[] args)
        {
            itemManager.GenerateItems(5);
            enemyManager.GenerateEnemies();
            manager.Draw();


            while (manager.play)
            {
                manager.Update();
                manager.Draw();
            }


            manager.EndGame();

        }
    }
}
