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
                int x = rand.Next(0, 5);
                int y = rand.Next(0, 5);
                x = x * 7 + 3;
                y = y * 7 + 3;
                if(ItemChecks(x, y) == null && player.PlayerCheck(x,y) == false && map.map[x,y] == ',')
                {
                    switch (rand.Next(0, 3))
                    {
                        case 0:
                            items.Add(new Item("heal", 3, x, y, player, rend));
                            break;
                        case 1:
                            items.Add(new Item("dmg", 1, x, y, player, rend));
                            break;
                        case 2:
                            items.Add(new Item("shield", 3, x, y, player, rend));
                            break;
                    }
                }
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
