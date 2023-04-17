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

        //private string message;

        public LoadManager loadManager;

        public StartScreen startScreen = new StartScreen();

        public EndScreen endScreen;

        public SoundManager soundManager = new SoundManager();


        public GameManager()
        {
            endScreen = new EndScreen(soundManager);
            render = new Render();
            mapGen = new MapGenerator();
            render.setGameManager(this);
            inputManager = new InputManager(this);
            map = new Map(mapGen.RandomizeMap(), render);
            exit = new Exit(this, render, map);
            itemManager = new ItemManager(map, render, this, exit, soundManager);
            enemyManager = new EnemyManager(map, render, itemManager, this, exit, soundManager);
            player = new Player(new Position((Constants.mapWidth/2) * Constants.roomWidth + (Constants.roomWidth/2), (Constants.mapHeight / 2) * Constants.roomHeight + (Constants.roomHeight / 2)), map, enemyManager, render, this, inputManager, itemManager, exit, soundManager);
            miniMap = new MiniMap(mapGen.makeMiniMap(), player);
            hud = new Hud(player, enemyManager, itemManager, this);
            cam = new Camera(player, this);
            loadManager = new LoadManager(this, render, cam, exit, itemManager, enemyManager, miniMap, player, hud, map, mapGen);
            
        }

        public void Update()
        {
            hud.SetMessage(" ");
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
            startScreen.Display();
            loadManager.FloorSetUp();
            while(play == true)
            {
                Update();
                Draw();
            }
            Conclusion(win);
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

        public void Conclusion(bool win)
        {
            if (win)
            {
                endScreen.Display(EndScreen.EndCon.Win);
            }
            else
            {
                endScreen.Display(EndScreen.EndCon.Lose);
            }
        }

    }
}
