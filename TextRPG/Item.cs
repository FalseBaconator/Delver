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

        private SoundManager soundManager;


        public Item(string name, Position pos, Render rend, SoundManager soundManager)
        {
            this.name = name;
            this.pos = pos;
            this.rend = rend;
            this.soundManager = soundManager;
            switch (name)
            {
                case var _ when name == GameManager.constants.healName:
                    sprite = GameManager.constants.healSprite;
                    break;
                case var _ when name == GameManager.constants.ATKBuffName:
                    sprite = GameManager.constants.ATKSprite;
                    break;
                case var _ when name == GameManager.constants.ShieldRepairName:
                    sprite = GameManager.constants.ShieldRepairSprite;
                    break;
            }

        }

        public void PickUp(Player player)    //Has the appropriate effect based on item
        {
            soundManager.Play(SoundManager.Noise.pickUp);
            switch (name)
            {
                case var _ when name == GameManager.constants.healName:
                    player.Heal(GameManager.constants.healAmount);
                    break;
                case var _ when name == GameManager.constants.ATKBuffName:
                    player.RaiseATK(GameManager.constants.ATKBuffAmount);
                    break;
                case var _ when name == GameManager.constants.ShieldRepairName:
                    player.RestoreShield(GameManager.constants.shieldRepairAmount);
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
