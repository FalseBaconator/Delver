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
        private int x;
        private int y;

        private char sprite;

        private ConsoleColor color;

        private Render rend;


        public Item(string name, int x, int y, Render rend)
        {
            this.name = name;
            this.x = x;
            this.y = y;
            this.rend = rend;
            switch (name)
            {
                case Constants.healName:
                    sprite = Constants.healSprite;
                    color = Constants.healColor;
                    break;
                case Constants.ATKBuffName:
                    sprite = Constants.ATKSprite;
                    color = Constants.ATKColor;
                    break;
                case Constants.ShieldRepairName:
                    sprite = Constants.ShieldRepairSprite;
                    color = Constants.ShieldRepairColor;
                    break;
            }
        }

        public void PickUp(Player player)    //Has the appropriate effect based on item
        {
            switch (name)
            {
                case Constants.healName:
                    player.Heal(Constants.healAmount);
                    break;
                case Constants.ATKBuffName:
                    player.RaiseATK(Constants.ATKBuffAmount);
                    break;
                case Constants.ShieldRepairName:
                    player.RestoreShield(Constants.shieldRepairAmount);
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
