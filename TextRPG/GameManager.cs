using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class GameManager
    {
        private bool play = true;

        //Define Objects
        static Render render = new Render();

        static MapGenerator mapGen = new MapGenerator();
        static Map map = new Map(mapGen.RandomizeMap(), render);

        static EnemyManager enemyManager;

        static Player player;

        static ItemManager itemManager;

        static InputManager inputManager;

        static Hud hud;

        private string message;

        public GameManager()
        {
            inputManager = new InputManager(this);
            itemManager = new ItemManager(map, render, this);
            enemyManager = new EnemyManager(map, render, itemManager, this);
            player = new Player(17, 17, map, enemyManager, render, this, inputManager, itemManager);
            hud = new Hud(player, enemyManager, 0, 36);
        }

        public void SetUp()                         //
        {                                           //
            itemManager.GenerateItems(player);      //  Set Up
            enemyManager.GenerateEnemies(player);   //
            Draw();                                 //
        }                                           //

        public void setMessage(string message)
        {
            this.message = message; //save message
            hud.SetMessage(message);    //set message in hud
        }

        public string GetMessage()
        {
            return message;
        }

        public void Update()
        {
            inputManager.Update();          //
            player.Update();                //  Update everything
            enemyManager.UpdateEnemies();   //

            if(player.isAlive() == false)   //
            {                               //  End game if player is dead
                play = false;               //
            }                               //
        }
        
        public void Play()
        {
            SetUp();
            while(play == true)
            {
                Update();
                Draw();
            }
            Conclusion();
        }

        public void Draw()  //Draw Everything
        {
            render.ResetBackgrounds();  //
            map.DrawMap();              //
            itemManager.Draw();         //  Set chars to arrays in rend
            player.Draw();              //
            enemyManager.DrawEnemies(); //
            hud.draw();     //Draws HUD
            render.DrawToScreen();    //Draws map and everything in
        }

        public void EndGame()
        {
            play = false;
        }

        public void Conclusion()
        {
            if (player.isAlive() && enemyManager.GetEnemyCount() == 0)  //
            {                                                           //
                Console.Clear();                                        //  Win
                Console.ResetColor();                                   //
                Console.Write("You Win!");                              //
                Console.ReadKey(true);                                  //
            }                                                           //
            else if (player.isAlive() == false)                     //
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
