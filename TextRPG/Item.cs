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
        private Position pos;

        private Tile sprite;

        private Render rend;


        public Item(string name, Position pos, Render rend)
        {
            this.name = name;
            this.pos = pos;
            this.rend = rend;
            switch (name)
            {
                case Constants.healName:
                    sprite = Constants.healSprite;
                    break;
                case Constants.ATKBuffName:
                    sprite = Constants.ATKSprite;
                    break;
                case Constants.ShieldRepairName:
                    sprite = Constants.ShieldRepairSprite;
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

        public bool isItemAt(Position pos)     //Returns true if item is on provided coords
        {
            bool check = false;
            if(this.pos == pos){
                check = true;
            }
            return check;
        }

        public Position GetPos()
        {
            return pos;
        }

        public void Draw()  //Draws item on map
        {
            rend.WholeMap[pos.y, pos.x] = sprite;
        }

        public string GetName()
        {
            return name;
        }

    }
}
