using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class GameCharacter
    {

        protected int x;
        protected int targetX;
        protected int y;
        protected int targetY;
        protected int HP;
        protected int maxHP;
        protected int ATK;
        protected char sprite;
        protected Map map;
        protected EnemyManager enemyManager;
        protected ConsoleColor color;
        protected Render rend;
        protected GameManager manager;

        protected bool attacked = false;

        protected bool alive = true;

        public GameCharacter(int x, int y, int HP, int ATK, char sprite, Map map, EnemyManager enemyManager, ConsoleColor color, Render rend, GameManager manager)
        {
            this.x = x;
            this.y = y;
            this.HP = HP;
            this.maxHP = HP;
            this.ATK = ATK;
            this.sprite = sprite;
            this.map = map;
            this.enemyManager = enemyManager;
            this.color = color;
            this.rend = rend;
            this.manager = manager;
        }

        public virtual void Draw()  //Assigns proper properties to rend arrays
        {
            if (attacked)
            {
                attacked = false;
                rend.BackgroundColors[y, x] = ConsoleColor.Red;
            }
            else
            {
                rend.BackgroundColors[y, x] = ConsoleColor.Black;
            }
            rend.ScreenColors[y, x] = color;
            rend.ScreenChars[y, x] = sprite;
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

        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }

        public bool isAlive()
        {
            return alive;
        }

    }
}
