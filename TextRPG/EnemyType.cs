using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class EnemyType
    {
        public int type;
        public int HP;
        public int ATK;
        public char sprite;
        public ConsoleColor color;

        public EnemyType(int type)
        {
            switch (type)
            {
                case 0:     //Slime
                    HP = 1;
                    ATK = 1;
                    sprite = 'O';
                    color = ConsoleColor.Blue;
                    break;
                case 1:     //Goblin
                    HP = 5;
                    ATK = 2;
                    sprite = 'X';
                    color = ConsoleColor.DarkGreen;
                    break;
                case 2:     //Kobold
                    HP = 3;
                    ATK = 1;
                    sprite = 'X';
                    color = ConsoleColor.DarkRed;
                    break;
            }
        }

    }
}
