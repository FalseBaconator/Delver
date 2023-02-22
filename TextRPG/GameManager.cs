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
            this.message = message; //save message
            hud.message = message;      //set message in hud
        }

        public void Update()
        {
            inputManager.Update();      //
            player.Update();            //  Update everything
            eManager.UpdateEnemies();   //

            if(player.alive == false)       //
            {                               //  End game if player is dead
                play = false;               //
            }                               //
        }

        public void Draw()  //Draw Everything
        {
            rend.ResetBackgrounds();    //
            map.DrawMap();              //
            itemManager.Draw();         //  Set chars to arrays in rend
            player.Draw();              //
            eManager.DrawEnemies();     //
            hud.draw();     //Draws HUD
            rend.DrawToScreen();    //Draws map and everything in
        }

        public void EndGame()
        {
            if (player.alive && eManager.Enemies.Count == 0)    //
            {                                                   //
                Console.ReadKey(true);                          //
                Console.Clear();                                //  Win
                Console.ResetColor();                           //
                Console.Write("You Win!");                      //
                Console.ReadKey(true);                          //
            }                                                   //
            else if (player.alive == false)                         //
            {                                                       //
                Console.ReadKey(true);                              //
                Console.Clear();                                    //  Lose
                Console.ResetColor();                               //
                Console.Write("You are dead, no big surprise");     //
                Console.ReadKey(true);                              //
            }                                                       //
            else if(inputManager.GetKey() == ConsoleKey.Escape) //
            {                                                   //
                Console.Clear();                                //
                Console.ResetColor();                           //  Quit
                Console.Write("Hope to see you again!");        //
                Console.ReadKey(true);                          //
            }                                                   //
            else                                                                                                                                //
            {                                                                                                                                   //
                Console.Clear();                                                                                                                //
                Console.ResetColor();                                                                                                           //  Shouldn't be possible
                Console.Write("I don't know how you saw this message, but contact RWohler and tell them what caused your playthrough to end."); //
                Console.ReadKey(true);                                                                                                          //
            }                                                                                                                                   //
        }

    }
}
