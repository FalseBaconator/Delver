using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class ItemManager
    {
        List<Item> items = new List<Item>();
        Random rand = new Random();

        Player player;
        Render rend;
        Map map;
        public GameManager gManager;

        public ItemManager(Player player, EnemyManager eManager, Map map, Render rend)
        {
            this.player = player;
            this.rend = rend;
            this.map = map;
            player.itemManager = this;
            eManager.itemManager = this;
        }

        public void GenerateItems(int ItemNum)
        {
            while(items.Count < ItemNum)
            {
                int x = rand.Next(0, 5);    //
                int y = rand.Next(0, 5);    //  Choses random map chunk and moves to center of chunk
                x = x * 7 + 3;              //
                y = y * 7 + 3;              //
                if(ItemChecks(x, y) == null && player.PlayerCheck(x,y) == false && map.map[x,y] == ',') //
                {                                                                                       //
                    switch (rand.Next(0, 3))                                                            //
                    {                                                                                   //
                        case 0:                                                                         //
                            items.Add(new Item("Healing Potion", 3, x, y, player, rend));               //  Generates a random item if spot isn't occupied
                            break;                                                                      //
                        case 1:                                                                         //
                            items.Add(new Item("ATK Buff", 1, x, y, player, rend));                     //
                            break;                                                                      //
                        case 2:                                                                         //
                            items.Add(new Item("Shield Repair", 3, x, y, player, rend));                //
                            break;                                                                      //
                    }                                                                                   //
                }                                                                                       //
            }
        }

        public Item ItemChecks(int x, int y)
        {
            Item found = null;
            foreach(Item item in items)
            {
                if (item.ItemCheck(x, y))
                {
                    found = item;
                }
            }

            return found;
        }

        public void PickUp(Item item)
        {
            if (items.Contains(item))
            {
                item.PickUp();
                items.Remove(item);
                gManager.setMessage("Player found " + item.name);
            }
        }

        public void Draw()
        {
            foreach(Item item in items)
            {
                item.Draw();
            }
        }

    }
}
