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
        static Render render = new Render();

        static MapGenerator mapGen = new MapGenerator();
        static Map map;
        static MiniMap miniMap;

        static EnemyManager enemyManager;

        static Player player;

        static ItemManager itemManager;

        static InputManager inputManager;

        static Hud hud;

        static Exit exit;

        static Camera cam;

        private string message;

        static int currentFloor = 1;

        public GameManager()
        {
            inputManager = new InputManager(this);
            map = new Map(mapGen.RandomizeMap(), render);
            exit = new Exit(this, render, map);
            itemManager = new ItemManager(map, render, this, exit);
            enemyManager = new EnemyManager(map, render, itemManager, this, exit);
            player = new Player((Constants.mapWidth/2) * Constants.roomWidth + (Constants.roomWidth/2), (Constants.mapHeight / 2) * Constants.roomHeight + (Constants.roomHeight / 2), map, enemyManager, render, this, inputManager, itemManager, exit);
            miniMap = new MiniMap(mapGen.makeMiniMap(), player);
            hud = new Hud(player, enemyManager, this);
            cam = new Camera(player);
        }

        public void SetUp()                         //
        {                                           //
            render.setHud(hud);                     //
            render.setCam(cam);                     //
            render.setMiniMap(miniMap);             //
            cam.Update();                           //
            exit.PlaceExit(player);                 //  SetUp
            itemManager.GenerateItems(player);      // 
            enemyManager.GenerateEnemies(player);   //
            miniMap.Update();                       //
            Draw();                                 //
        }                                           //

        public void NextFloor()
        {
            currentFloor++;
            map = new Map(mapGen.RandomizeMap(), render);
            exit = new Exit(this, render, map);
            itemManager = new ItemManager(map, render, this, exit);
            enemyManager = new EnemyManager(map, render, itemManager, this, exit);
            hud = new Hud(player, enemyManager, this);
            player.placePlayer((Constants.mapWidth / 2) * Constants.roomWidth + (Constants.roomWidth / 2), (Constants.mapHeight / 2) * Constants.roomHeight + (Constants.roomHeight / 2));
            player.SetForNextFloor(map, enemyManager, itemManager, exit);
            miniMap = new MiniMap(mapGen.makeMiniMap(), player);
            SetUp();
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

        public int getFloor()
        {
            return currentFloor;
        }

        public void Update()
        {
            inputManager.Update();          //
            player.Update();                //  Update everything
            cam.Update();                   //
            enemyManager.UpdateEnemies();   //
            miniMap.Update();               //
            
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
