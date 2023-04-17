using System;
using System.Collections.Generic;

namespace TextRPG
{
    internal class ItemManager
    {
        private List<Item> items = new List<Item>();
        private Item[,] itemMap = new Item[Constants.mapHeight * Constants.roomHeight, Constants.mapWidth * Constants.roomWidth];
        private Random rand = Constants.rand;

        private Render rend;
        private Map map;
        private GameManager gManager;
        private Exit exit;
        private Hud hud;
        private SoundManager soundManager;

        public void SetHud(Hud hud)
        {
            this.hud = hud;
        }

        public ItemManager(Map map, Render rend, GameManager gManager, Exit exit, SoundManager soundManager)
        {
            this.rend = rend;
            this.map = map;
            this.gManager = gManager;
            this.exit = exit;
            this.soundManager = soundManager;
        }

        public void GenerateItems(Player player)
        {
            items = new List<Item>();
            itemMap = new Item[Constants.mapHeight * Constants.roomHeight, Constants.mapWidth * Constants.roomWidth];
            if (Globals.currentFloor < Constants.BossFloor)
            {
                while (items.Count < Constants.itemAmount)
                {
                    int x = rand.Next(Constants.mapWidth * Constants.roomWidth);        //
                    int y = rand.Next(Constants.mapHeight * Constants.roomHeight);      //  Choses random map spot
                    if (ItemAt(new Position(x, y)) == null && (Math.Abs(player.GetPos().x - x) > 5 || Math.Abs(player.GetPos().y - y) > 5) && map.isFloorAt(new Position(x, y)) && exit.isExitAt(new Position(x,y), false) == false) //
                    {
                        Position pos = new Position(x, y);                                                                                                                      //
                        switch (rand.Next(0, 2))                                                                                                                                //
                        {                                                                                                                                                       //
                            case 0:                                                                                                                                             //
                                items.Add(new Item(Constants.healName, pos, rend, soundManager));                                                                                            //  Generates a random item if spot isn't occupied
                                itemMap[x, y] = items[items.Count - 1];
                                break;                                                                                                                                          //
                            case 1:                                                                                                                                             //
                                items.Add(new Item(Constants.ShieldRepairName, pos, rend, soundManager));                                                                                         //
                                itemMap[x, y] = items[items.Count - 1];
                                break;                                                                                                                                   //
                        }                                                                                                                                                       //
                    }                                                                                                                                                           //
                }
            }else if(Globals.currentFloor == Constants.BossFloor)
            {
                while (items.Count < Constants.bossItemAmount)
                {
                    int x = rand.Next(Constants.BossRoomWidth);         //
                    int y = rand.Next(Constants.BossRoomHeight);        //  Choses random map spot
                    if (ItemAt(new Position(x,y)) == null && (Math.Abs(player.GetPos().x - x) > 2 || Math.Abs(player.GetPos().y - y) > 2) && map.isFloorAt(new Position(x, y)) && exit.isExitAt(new Position(x, y), false) == false) //
                    {
                        Position pos = new Position(x, y);                                                                                                                      //
                        switch (rand.Next(0, 2))                                                                                                                                //
                        {                                                                                                                                                       //
                            case 0:                                                                                                                                             //
                                items.Add(new Item(Constants.healName, pos, rend, soundManager));                                                                                            //  Generates a random item if spot isn't occupied
                                itemMap[x, y] = items[items.Count - 1];
                                break;                                                                                                                                          //
                            case 1:                                                                                                                                             //
                                items.Add(new Item(Constants.ShieldRepairName, pos, rend, soundManager));                                                                                    //
                                itemMap[x, y] = items[items.Count - 1];
                                break;                                                                                                                                          //
                        }                                                                                                                                                       //
                    }                                                                                                                                                           //
                }
            }
        }

        public Item ItemAt(Position pos)    //Returns item at provided coords
        {
            return itemMap[pos.x, pos.y];
        }

        public void PickUp(Item item, Player player)   //Uses provided item
        {
            if (items.Contains(item))
            {
                item.PickUp(player);
                items.Remove(item);
                itemMap[item.GetPos().x, item.GetPos().y] = null;
                hud.SetMessage("You found " + item.GetName());
            }
        }

        public void Draw()  //puts items in rend arrays
        {
            foreach(Item item in items)
            {
                item.Draw();
            }
        }

    }
}
