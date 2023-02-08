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

        public GameManager(Player player, EnemyManager eManager, Map map, InputManager inputManager, ItemManager itemManager, Render rend)
        {
            this.player = player;
            this.eManager = eManager;
            this.map = map;
            this.inputManager = inputManager;
            inputManager.manager = this;
            this.itemManager = itemManager;
            this.rend = rend;
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
            map.DrawMap();
            itemManager.Draw();
            player.Draw();
            player.DisplayHud();
            eManager.DrawEnemies();
            rend.DrawToScreen();
        }

    }
}
