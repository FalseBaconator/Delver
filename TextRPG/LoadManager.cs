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

        private Quests quests;

        public LoadManager(GameManager gManager, Render render, Camera cam, Exit exit, ItemManager itemManager, EnemyManager enemyManager, ShopKeepManager shopKeepManager, MiniMap miniMap, Player player, Hud hud, Map map, MapGenerator mapGen, Quests quests)
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
            this.quests = quests;
            Globals.currentFloor = 1;
        }

        public void FloorSetUp()                    //
        {
            //Console.WriteLine("A");
            Globals.round = 0;
            render.setHud(hud);                     //
            render.setCam(cam);                     //
            render.setMiniMap(miniMap);             //
            //Console.WriteLine("B");
            cam.Update();                           //
            exit.PlaceExit(player);                 //  SetUp
            itemManager.GenerateItems(player);      //
            //Console.WriteLine("C");
            enemyManager.GenerateEnemies(player);   //
            shopKeepManager.GenerateShopKeeps(player);
            miniMap.Update();                       //
            gManager.Draw();                        //
            //Console.WriteLine("D");
        }                                           //

        public void BossSetUp()
        {
            Globals.round = 0;
            shopKeepManager.ClearShopKeeps();
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
            if (Globals.currentFloor == GameManager.constants.BossFloor)
            {
                map.NewMap(mapGen.BossRoom());
                player.placePlayer(new Position(GameManager.constants.BossRoomWidth / 2, GameManager.constants.BossRoomHeight / 2));
            }
            else
            {
                map.NewMap(mapGen.RandomizeMap());
                player.placePlayer(new Position((GameManager.constants.mapWidth / 2) * GameManager.constants.roomWidth + (GameManager.constants.roomWidth / 2), (GameManager.constants.mapHeight / 2) * GameManager.constants.roomHeight + (GameManager.constants.roomHeight / 2)));
            }
            miniMap.Refresh(mapGen.makeMiniMap());
            if (Globals.currentFloor == GameManager.constants.BossFloor)
                BossSetUp();
            else
                FloorSetUp();
        }
    }
}
