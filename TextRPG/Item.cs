using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Item
    {
        private string name; //Healing Potion, ATK Buff, Shield Repair
        private int value;
        private int x;
        private int y;

        private char sprite;

        private ConsoleColor color;

        private Render rend;


        public Item(string name, int value, int x, int y, Render rend)
        {
            this.name = name;
            this.value = value;
            this.x = x;
            this.y = y;
            //this.player = player;
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

        public void PickUp(Player player)    //Has the appropriate effect based on item
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

        public bool isItemAt(int x, int y)     //Returns true if item is on provided coords
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

        public string GetName()
        {
            return name;
        }

    }
}
