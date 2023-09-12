using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class LoadManager
    {
        private GameManager gManager;

        private Render render;

        private Camera cam;

        private Exit exit;

        private ItemManager itemManager;

        private EnemyManager enemyManager;

        private ShopKeepManager shopKeepManager;

        private MiniMap miniMap;

        private Player player;

        private Hud hud;

        private Map map;

        private MapGenerator mapGen;

        public LoadManager(GameManager gManager, Render render, Camera cam, Exit exit, ItemManager itemManager, EnemyManager enemyManager, ShopKeepManager shopKeepManager, MiniMap miniMap, Player player, Hud hud, Map map, MapGenerator mapGen)
        {
            this.gManager = gManager;
            this.render = render;
            this.cam = cam;
            this.exit = exit;
            this.itemManager = itemManager;
            this.enemyManager = enemyManager;
            this.shopKeepManager = shopKeepManager;
            this.miniMap = miniMap;
            this.player = player;
            this.hud = hud;
            this.map = map;
            this.mapGen = mapGen;
            Globals.currentFloor = 1;
        }

        public void FloorSetUp()                    //
        {                                           //
            render.setHud(hud);                     //
            render.setCam(cam);                     //
            render.setMiniMap(miniMap);             //
            cam.Update();                           //
            exit.PlaceExit(player);                 //  SetUp
            itemManager.GenerateItems(player);      //
            enemyManager.GenerateEnemies(player);   //
            shopKeepManager.GenerateShopKeeps(player);
            miniMap.Update();                       //
            gManager.Draw();                        //
        }                                           //

        public void BossSetUp()
        {
            render.setHud(hud);
            render.setCam(cam);
            cam.Update();
            exit.hide();
            itemManager.GenerateItems(player);
            enemyManager.GenerateBoss(player);
            gManager.Draw();
        }

        public void NextFloor()
        {
            Globals.currentFloor++;
            if (Globals.currentFloor == Constants.BossFloor)
            {
                map.NewMap(mapGen.BossRoom());
                player.placePlayer(new Position(Constants.BossRoomWidth / 2, Constants.BossRoomHeight / 2));
            }
            else
            {
                map.NewMap(mapGen.RandomizeMap());
                player.placePlayer(new Position((Constants.mapWidth / 2) * Constants.roomWidth + (Constants.roomWidth / 2), (Constants.mapHeight / 2) * Constants.roomHeight + (Constants.roomHeight / 2)));
            }
            miniMap.Refresh(mapGen.makeMiniMap());
            if (Globals.currentFloor == Constants.BossFloor)
                BossSetUp();
            else
                FloorSetUp();
        }



    }
}
