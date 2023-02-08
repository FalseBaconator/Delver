using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Item
    {
        string name; //heal, dmg, shield
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
                case "heal":
                    sprite = '+';
                    color = ConsoleColor.Green;
                    break;
                case "dmg":
                    sprite = '*';
                    color = ConsoleColor.Red;
                    break;
                case "shield":
                    sprite = '#';
                    color = ConsoleColor.Blue;
                    break;
            }
        }

        public void PickUp()
        {
            switch (name)
            {
                case "heal":
                    player.Heal(value);
                    break;
                case "dmg":
                    player.RaiseATK(value);
                    break;
                case "shield":
                    player.RestoreShield(value);
                    break;
            }
        }

        public bool ItemCheck(int x, int y)
        {
            bool check = false;
            if(this.x == x && this.y == y){
                check = true;
            }
            return check;
        }

        public void Draw()
        {
            rend.ScreenChars[x, y] = sprite;
            rend.ScreenColors[x, y] = color;
        }

    }
}
