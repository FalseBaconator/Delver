using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    static class Constants
    {
        //Player Settings
        public const int playerBaseHP = 5;
        public const int playerBaseShield = 5;
        public const int playerBaseAttack = 2;
        public const char playerSprite = '@';
        public const ConsoleColor playerColor = ConsoleColor.White;

        //Enemy Settings
        public const int EnemySightRange = 5;

        //Slime Settings
        public const int slimeBaseHP = 1;
        public const int slimeBaseAttack = 1;
        public const char slimeSprite = 'O';
        public const ConsoleColor slimeColor = ConsoleColor.Cyan;
        public const string slimeName = "Slime";

        //Goblin Settings
        public const int goblinBaseHP = 5;
        public const int goblinBaseAttack = 2;
        public const char goblinSprite = 'X';
        public const ConsoleColor goblinColor = ConsoleColor.DarkGreen;
        public const string goblinName = "Goblin";

        //Kobold Settings
        public const int koboldBaseHP = 3;
        public const int koboldBaseAttack = 1;
        public const char koboldSprite = 'X';
        public const ConsoleColor koboldColor = ConsoleColor.DarkRed;
        public const string koboldName = "Kobold";

        //Item Settings
        public const int itemAmount = 5;
        public const int healAmount = 3;
        public const int ATKBuffAmount = 1;
        public const int shieldRepairAmount = 3;
        public const char healSprite = '+';
        public const char ATKSprite = '*';
        public const char ShieldRepairSprite = '#';
        public const ConsoleColor healColor = ConsoleColor.Green;
        public const ConsoleColor ATKColor = ConsoleColor.Red;
        public const ConsoleColor ShieldRepairColor = ConsoleColor.Blue;
        public const string healName = "Health Potion";
        public const string ATKBuffName = "ATK Buff";
        public const string ShieldRepairName = "Shield Repair";

        //Map Settings
        public const int mapHeight = 8;
        public const int mapWidth = 9;
        public const int roomHeight = 7;
        public const int roomWidth = 7;

    }
}
