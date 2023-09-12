using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class ShopKeepManager
    {
        private List<ShopKeep> shopKeeps = new List<ShopKeep>();
        private ShopKeep[,] shopKeepMap = new ShopKeep[Constants.mapHeight * Constants.roomHeight, Constants.mapWidth * Constants.roomWidth];
        private Map map;
        private Random random = Constants.rand;
        private bool toMove;
        private Render rend;
        private ItemManager itemManager;
        private ShopKeep lastAttacked;
        GameManager manager;
        Exit exit;
        private Hud hud;
        private SoundManager soundManager;
        private EnemyManager enemyManager;


        public void SetHud(Hud hud)
        {
            this.hud = hud;
        }

        public ShopKeepManager(Map map, Render rend, ItemManager itemManager, GameManager manager, Exit exit, SoundManager soundManager, EnemyManager enemyManager)
        {
            this.map = map;
            this.rend = rend;
            this.itemManager = itemManager;
            this.manager = manager;
            this.exit = exit;
            this.soundManager = soundManager;
            this.enemyManager = enemyManager;
        }

        public void GenerateShopKeeps(Player player)
        {
            Position tempPos;
            ClearShopKeeps();
            int placedShopKeeps = 0;
            while (placedShopKeeps < Constants.ShopKeepAmount)
            {
                tempPos = new Position(random.Next(Constants.mapWidth * Constants.roomWidth), random.Next(Constants.mapHeight * Constants.roomHeight));
                if ((Math.Abs(player.GetPos().x - tempPos.x) > 5 || Math.Abs(player.GetPos().y - tempPos.y) > 5) && map.isFloorAt(tempPos) && itemManager.ItemAt(tempPos) == null && exit.isExitAt(tempPos, false) == false && ShopKeepAt(tempPos) == null)
                {
                    shopKeeps.Add(new ShopKeep(tempPos, map, player, enemyManager, itemManager, rend, manager, hud, exit, soundManager, this));
                    placedShopKeeps++;
                    shopKeepMap[tempPos.x, tempPos.y] = shopKeeps[placedShopKeeps - 1];
                }
            }
        }

        public void UpdateShopKeeps() //Move each shop keep on every other turn
        {
            if (toMove)
            {
                foreach (ShopKeep shopKeep in shopKeeps)
                {
                    shopKeepMap[shopKeep.GetPos().x, shopKeep.GetPos().y] = null;
                    shopKeep.Update();
                    shopKeepMap[shopKeep.GetPos().x, shopKeep.GetPos().y] = shopKeep;
                }
            }
            toMove = !toMove;
        }

        public ShopKeep ShopKeepAt(Position pos)    //Returns the shop keep at the provided coords. Saves lastAttacked shop keep if attacking
        {
            ShopKeep foundShopKeep = shopKeepMap[pos.x, pos.y];
            return foundShopKeep;
        }

        public void DrawShopKeeps()   //Save shop keeps to rend arrays
        {
            foreach (ShopKeep shopKeep in shopKeeps)
            {
                shopKeep.Draw();
            }
        }

        public void RemoveShopKeep(ShopKeep shopKeep)    //Removes shop keep from shop keeps array
        {
            if (shopKeeps.Contains(shopKeep))
            {
                shopKeeps.Remove(shopKeep);
                shopKeepMap[shopKeep.GetPos().x, shopKeep.GetPos().y] = null;
            }
        }

        public int GetShopKeepCount()
        {
            return shopKeeps.Count();
        }

        public ShopKeep GetLastAttacked()
        {
            return lastAttacked;
        }

        public void ClearShopKeeps()
        {
            foreach (ShopKeep shopKeep in shopKeeps)
            {
                shopKeepMap[shopKeep.GetPos().x, shopKeep.GetPos().y] = null;
            }
            shopKeeps.Clear();
        }
    }
}