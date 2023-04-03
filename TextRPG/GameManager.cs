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
        private bool win = false;

        //Define Objects
        static Render render;

        public static MapGenerator mapGen;
        public static Map map;
        public static MiniMap miniMap;

        public static EnemyManager enemyManager;

        static Player player;

        public static ItemManager itemManager;

        static InputManager inputManager;

        public static Hud hud;

        public static Exit exit;

        static Camera cam;

        private string message;

        public LoadManager loadManager;

        public GameManager()
        {
            render = new Render();
            mapGen = new MapGenerator();
            render.setGameManager(this);
            inputManager = new InputManager(this);
            map = new Map(mapGen.RandomizeMap(), render);
            exit = new Exit(this, render, map);
            itemManager = new ItemManager(map, render, this, exit);
            enemyManager = new EnemyManager(map, render, itemManager, this, exit);
            player = new Player(new Position((Constants.mapWidth/2) * Constants.roomWidth + (Constants.roomWidth/2), (Constants.mapHeight / 2) * Constants.roomHeight + (Constants.roomHeight / 2)), map, enemyManager, render, this, inputManager, itemManager, exit);
            miniMap = new MiniMap(mapGen.makeMiniMap(), player);
            hud = new Hud(player, enemyManager, this);
            cam = new Camera(player, this);
            loadManager = new LoadManager(this, render, cam, exit, itemManager, enemyManager, miniMap, player, hud, map, mapGen);
            
        }


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
            setMessage(" ");
            if(player.isAlive() == false)   //
            {                               //  End game if player is dead
                play = false;               //
            }                               //
            
            inputManager.Update();          //
            player.Update();                //  Update everything
            cam.Update();                   //
            enemyManager.UpdateEnemies();   //
            miniMap.Update();               //
        }
        
        public void Play()
        {
            loadManager.FloorSetUp();
            while(play == true)
            {
                Update();
                Draw();
            }
            Conclusion();
        }

        public void Draw()  //Draw Everything
        {
            //render.ResetBackgrounds();  //
            map.DrawMap();              //
            itemManager.Draw();         //  Set chars to arrays in rend
            player.Draw();              //
            enemyManager.DrawEnemies(); //
            exit.Draw();                //
            hud.draw();                 //
            render.DrawToScreen();    //Adds to screen
        }

        public void EndGame(bool win)
        {
            this.win = win;
            play = false;
        }

        public void Conclusion()
        {
            if (win)                        //
            {                               //
                Console.Clear();            //  Win
                Console.ResetColor();       //
                Console.Write("You Win!");  //
                Console.ReadKey(true);      //
            }                               //
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
