using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Item
    {
        public string name; //Healing Potion, ATK Buff, Shield Repair
        int value;
        int x;
        int y;

        Player player;

        char sprite;

        ConsoleColor color;

        Render rend;


        public Item(string name, int value, int x, int y, Player player, Render rend)
        {
            this.name = name;
            this.value = value;
            this.x = x;
            this.y = y;
            this.player = player;
            this.rend = rend;
            switch (name)
            {
                case "Healing Potion":
                    sprite = '+';
                    color = ConsoleColor.Green;
                    break;
                case "ATK Buff":
                    sprite = '*';
                    color = ConsoleColor.Red;
                    break;
                case "Shield Repair":
                    sprite = '#';
                    color = ConsoleColor.Blue;
                    break;
            }
        }

        public void PickUp()    //Has the appropriate effect based on item
        {
            switch (name)
            {
                case "Healing Potion":
                    player.Heal(value);
                    break;
                case "ATK Buff":
                    player.RaiseATK(value);
                    break;
                case "Shield Repair":
                    player.RestoreShield(value);
                    break;
            }
        }

        public bool ItemCheck(int x, int y)     //Returns true if item is on provided coords
        {
            bool check = false;
            if(this.x == x && this.y == y){
                check = true;
            }
            return check;
        }

        public void Draw()  //Draws item on map
        {
            rend.ScreenChars[y, x] = sprite;
            rend.ScreenColors[y, x] = color;
        }

    }
}
