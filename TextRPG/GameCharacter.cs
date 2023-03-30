using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class GameCharacter
    {

        protected Position pos;
        protected Position targetPos;
        protected int HP;
        protected int maxHP;
        protected int ATK;
        protected Tile sprite;
        protected Map map;
        protected EnemyManager enemyManager;
        protected Render rend;
        protected GameManager manager;

        protected bool attacked = false;

        protected bool alive = true;

        public GameCharacter(Position pos, int HP, int ATK, Tile sprite, Map map, EnemyManager enemyManager, Render rend, GameManager manager)
        {
            this.pos = pos;
            this.HP = HP;
            this.maxHP = HP;
            this.ATK = ATK;
            this.sprite = sprite;
            this.map = map;
            this.enemyManager = enemyManager;
            this.rend = rend;
            this.manager = manager;
        }

        public virtual void Draw()  //Assigns proper properties to rend arrays
        {
            if (attacked)
            {
                attacked = false;
                sprite.backgroundColor = ConsoleColor.Red;
            }
            else if(sprite.backgroundColor != Constants.BGColor)
            {
                sprite.backgroundColor = Constants.BGColor;
            }
            rend.WholeMap[pos.y, pos.x] = sprite;
        }

        public virtual void TakeDMG(int DMG)    //Takes DMG and may kill character
        {
            HP -= DMG;
            attacked = true;
            if (HP <= 0)
            {
                HP = 0;
                alive = false;
            }
        }
        public int GetHealth()
        {
            return HP;
        }

        public int GetMaxHealth()
        {
            return maxHP;
        }

        public int GetATK()
        {
            return ATK;
        }

        public Position GetPos()
        {
            return pos;
        }

        public bool isAlive()
        {
            return alive;
        }

    }
}
