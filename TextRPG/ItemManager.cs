using System;
using System.Collections.Generic;

namespace TextRPG
{
    internal class ItemManager
    {
        private List<Item> items = new List<Item>();
        private Random rand = new Random();

        private Render rend;
        private Map map;
        private GameManager gManager;

        public ItemManager(Map map, Render rend, GameManager gManager)
        {
            this.rend = rend;
            this.map = map;
            this.gManager = gManager;
        }

        public void GenerateItems(Player player)
        {
            while(items.Count < Constants.itemAmount)
            {
                int x = rand.Next(Constants.mapWidth * Constants.roomWidth);        //
                int y = rand.Next(Constants.mapHeight * Constants.roomHeight);      //  Choses random map spot
                if(ItemAt(x, y) == null && Math.Abs(player.GetX() - x) > 5 && Math.Abs(player.GetY() - y) > 5 && map.isFloorAt(x,y))    //
                {                                                                                                                       //
                    switch (rand.Next(0, 3))                                                                                            //
                    {                                                                                                                   //
                        case 0:                                                                                                         //
                            items.Add(new Item(Constants.healName, x, y, rend));                                                        //  Generates a random item if spot isn't occupied
                            break;                                                                                                      //
                        case 1:                                                                                                         //
                            items.Add(new Item(Constants.ATKBuffName, x, y, rend));                                                     //
                            break;                                                                                                      //
                        case 2:                                                                                                         //
                            items.Add(new Item(Constants.ShieldRepairName, x, y, rend));                                                //
                            break;                                                                                                      //
                    }                                                                                                                   //
                }                                                                                                                       //
            }
        }

        public Item ItemAt(int x, int y)    //Returns item at provided coords
        {
            Item found = null;
            foreach(Item item in items)
            {
                if (item.isItemAt(x, y))
                {
                    found = item;
                }
            }

            return found;
        }

        public void PickUp(Item item, Player player)   //Uses provided item
        {
            if (items.Contains(item))
            {
                item.PickUp(player);
                items.Remove(item);
                gManager.setMessage("Player found " + item.GetName());
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
