using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class EnemyType
    {
        public enum Type
        {
            slime,
            goblin,
            kobold
        }


        //public int type;
        public Type type;

        public int HP;
        public int ATK;
        public char sprite;
        public ConsoleColor color;

        public EnemyType(Type type)
        {
            this.type = type;
            switch (this.type)
            {
                case Type.slime:
                    HP = 1;
                    ATK = 1;
                    sprite = 'O';
                    color = ConsoleColor.Cyan;
                    break;
                case Type.goblin:
                    HP = 5;
                    ATK = 2;
                    sprite = 'X';
                    color = ConsoleColor.DarkGreen;
                    break;
                case Type.kobold:
                    HP = 3;
                    ATK = 1;
                    sprite = 'X';
                    color = ConsoleColor.DarkRed;
                    break;
            }
        }

    }
}
