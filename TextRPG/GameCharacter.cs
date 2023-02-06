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
        public int ATK;
        public char sprite;
        public Map map;
        public EnemyManager enemyManager;
        public ConsoleColor color;
        public Render rend;

        public bool alive = true;

        public GameCharacter(int x, int y, int HP, int ATK, char sprite, Map map, EnemyManager enemyManager, ConsoleColor color, Render rend)
        {
            this.x = x;
            this.y = y;
            this.HP = HP;
            this.ATK = ATK;
            this.sprite = sprite;
            this.map = map;
            this.enemyManager = enemyManager;
            this.color = color;
            this.rend = rend;
        }

        public void Draw()
        {
            rend.ScreenColors[x, y] = color;
            rend.ScreenChars[x, y] = sprite;
        }

        public virtual void TakeDMG(int DMG)
        {
            HP -= DMG;
            if (HP <= 0)
            {
                map.DrawTile(x, y);
                alive = false;
            }
        }

    }
}
