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

        public bool alive = true;

        public GameCharacter(int x, int y, int HP, int ATK, char sprite, Map map, EnemyManager enemyManager, ConsoleColor color)
        {
            this.x = x;
            this.y = y;
            this.HP = HP;
            this.ATK = ATK;
            this.sprite = sprite;
            this.map = map;
            this.enemyManager = enemyManager;
            this.color = color;
            Console.SetCursorPosition(this.x, this.y);
            Console.ForegroundColor = color;
            Console.Write(sprite);
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
