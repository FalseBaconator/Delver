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

        public Type type;

        public int HP;
        public int ATK;
        public string name;
        public char sprite;
        public ConsoleColor color;

        public EnemyType(Type type) //  Premade enemy types to be used by EnemyManager upon generating enemies
        {
            this.type = type;
            switch (this.type)
            {
                case Type.slime:
                    HP = 1;
                    ATK = 1;
                    name = "Slime";
                    sprite = 'O';
                    color = ConsoleColor.Cyan;
                    break;
                case Type.goblin:
                    HP = 5;
                    ATK = 2;
                    name = "Goblin";
                    sprite = 'X';
                    color = ConsoleColor.DarkGreen;
                    break;
                case Type.kobold:
                    HP = 3;
                    ATK = 1;
                    name = "Kobold";
                    sprite = 'X';
                    color = ConsoleColor.DarkRed;
                    break;
            }
        }

    }
}
