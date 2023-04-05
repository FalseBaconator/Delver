using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    static class Constants
    {
        //Random
        public static Random rand = new Random();

        //Player Settings
        public const int playerBaseHP = 5;
        public const int playerBaseShield = 5;
        public const int playerBaseAttack = 2;
        public static Tile playerSprite = new Tile('@', ConsoleColor.White, ConsoleColor.Black);

        //Enemy Settings
        public const int EnemySightRange = 5;
        public const int EnemyAmount = 50;

        //Slime Settings
        public const int slimeBaseHP = 1;
        public const int slimeBaseAttack = 1;
        public static Tile slimeSprite = new Tile('O', ConsoleColor.Cyan, ConsoleColor.Black);
        public const string slimeName = "Slime";

        //Goblin Settings
        public const int goblinBaseHP = 5;
        public const int goblinBaseAttack = 2;
        public static Tile goblinSprite = new Tile('X', ConsoleColor.DarkGreen, ConsoleColor.Black);
        public const string goblinName = "Goblin";

        //Kobold Settings
        public const int koboldBaseHP = 3;
        public const int koboldBaseAttack = 1;
        public static Tile koboldSprite = new Tile('X', ConsoleColor.DarkRed, ConsoleColor.Black);
        public const string koboldName = "Kobold";

        //Boss Settings
        public const int bossBaseHP = 200;
        public const int bossBaseAttack = 3;
        public static Tile bossSprite = new Tile('M', ConsoleColor.DarkRed, ConsoleColor.Black);
        public const string bossName = "Boss";

        //Item Settings
        public const int itemAmount = 50;
        public const int bossItemAmount = 10;
        public const int healAmount = 3;
        public const int ATKBuffAmount = 1;
        public const int shieldRepairAmount = 3;
        public static Tile healSprite = new Tile('+', ConsoleColor.Green, ConsoleColor.Black);
        public static Tile ATKSprite = new Tile('*', ConsoleColor.Red, ConsoleColor.Black);
        public static Tile ShieldRepairSprite = new Tile('#', ConsoleColor.Blue, ConsoleColor.Black);
        public const string healName = "Health Potion";
        public const string ATKBuffName = "ATK Buff";
        public const string ShieldRepairName = "Shield Repair";

        //Map Settings
        public const ConsoleColor mapColor = ConsoleColor.DarkGray;
        public const int mapHeight = 5;
        public const int mapWidth = 5;
        public const int roomHeight = 13;
        public const int roomWidth = 13;
        public const int BossRoomWidth = 15;
        public const int BossRoomHeight = 15;
        public const int BossFloor = 3;
        public const int RoomsPerCategory = 4;

        //Cam Settings
        public const int camSize = 15;

        //Render Settings
        public const int rendWidth = 35;
        public const int rendHeight = 50;
        public const ConsoleColor borderColor = ConsoleColor.White;
        public const ConsoleColor BGColor = ConsoleColor.Black;

        //HUD Settings
        public const int hudWidth = 33;
        public const int messageBoxHeight = 4;
        public const int statsHeight = 6;
        public const string playerStatsList = "Player|HP: X|SHLD: Y|ATK: Z|FLR: $/^";
        public const string enemyStatsList = "HP: X|ATK: Z";

        //Exit Settings
        public static Tile exitSprite = new Tile('¤', ConsoleColor.Yellow, ConsoleColor.Black);

    }
}
