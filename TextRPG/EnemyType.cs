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
                    HP = Constants.slimeBaseHP;
                    ATK = Constants.slimeBaseAttack;
                    name = Constants.slimeName;
                    sprite = Constants.slimeSprite;
                    color = Constants.slimeColor;
                    break;
                case Type.goblin:
                    HP = Constants.goblinBaseHP;
                    ATK = Constants.goblinBaseAttack;
                    name = Constants.goblinName;
                    sprite = Constants.goblinSprite;
                    color = Constants.goblinColor;
                    break;
                case Type.kobold:
                    HP = Constants.koboldBaseHP;
                    ATK = Constants.koboldBaseAttack;
                    name = Constants.koboldName;
                    sprite = Constants.koboldSprite;
                    color = Constants.koboldColor;
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
