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

        private Type type;

        private int HP;
        private int ATK;
        private string name;
        private char sprite;
        private ConsoleColor color;

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
        
        public int GetHP()
        {
            return HP;
        }

        public int GetATK()
        {
            return ATK;
        }

        public string GetName()
        {
            return name;
        }

        public char GetSprite()
        {
            return sprite;
        }

        public ConsoleColor GetColor()
        {
            return color;
        }

        public Type GetEnemyType()
        {
            return type;
        }

    }
}
