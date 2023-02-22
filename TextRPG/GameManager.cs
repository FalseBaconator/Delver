using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class GameManager
    {
        public bool play = true;

        Player player;

        EnemyManager eManager;

        Map map;

        InputManager inputManager;

        ItemManager itemManager;

        Render rend;

        Hud hud;

        public string message;

        public GameManager(Player player, EnemyManager eManager, Map map, InputManager inputManager, ItemManager itemManager, Render rend, Hud hud)
        {
            this.player = player;
            player.gManager = this;
            this.eManager = eManager;
            eManager.gManager = this;
            this.map = map;
            this.inputManager = inputManager;
            inputManager.manager = this;
            this.itemManager = itemManager;
            itemManager.gManager = this;
            this.rend = rend;
            this.hud = hud;
        }

        public void setMessage(string message)
        {
            this.message = message;
            hud.message = message;
        }

        public void Update()
        {
            inputManager.Update();
            player.Update();
            eManager.UpdateEnemies();
            if(player.alive == false)
            {
                play = false;
            }
        }

        public void Draw()
        {
            rend.ResetBackgrounds();
            map.DrawMap();
            itemManager.Draw();
            player.Draw();
            eManager.DrawEnemies();
            hud.draw();
            rend.DrawToScreen();
        }

        public void EndGame()
        {
            if (player.alive && eManager.Enemies.Count == 0)
            {
                Console.ReadKey(true);
                Console.Clear();
                Console.ResetColor();
                Console.Write("You Win!");
                Console.ReadKey(true);
            }
            else if (player.alive == false)
            {
                Console.ReadKey(true);
                Console.Clear();
                Console.ResetColor();
                Console.Write("You are dead, no big surprise");
                Console.ReadKey(true);
            }
            else if(inputManager.GetKey() == ConsoleKey.Escape)
            {
                Console.Clear();
                Console.ResetColor();
                Console.Write("Hope to see you again!");
                Console.ReadKey(true);
            }
            else
            {
                Console.Clear();
                Console.ResetColor();
                Console.Write("I don't know how you saw this message, but contact RWohler and tell them what caused your playthrough to end.");
                Console.ReadKey(true);
            }
        }

    }
}
