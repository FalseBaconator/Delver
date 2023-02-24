using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class GameCharacter
    {

        public int x;
        public int y;
        public int HP;
        public int maxHP;
        public int ATK;
        public char sprite;
        public Map map;
        public EnemyManager enemyManager;
        public ConsoleColor color;
        public Render rend;

        private bool attacked = false;

        public bool alive = true;

        public GameCharacter(int x, int y, int HP, int ATK, char sprite, Map map, EnemyManager enemyManager, ConsoleColor color, Render rend)
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

    }
}
