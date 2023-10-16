using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    class Constants
    {
        //Random
        public readonly Random rand = new Random();

        //Player Settings
        public readonly int playerBaseHP = 5;
        public readonly int playerBaseShield = 5;
        public readonly int playerBaseAttack = 2;
        public readonly int playerXPThreshold = 25;
        public readonly Tile playerSprite = new Tile('@', ConsoleColor.White, ConsoleColor.Black);

        //Enemy Settings
        public readonly int EnemySightRange = 7;
        public readonly int EnemyAmount = 50;

        //ShopKeep Settings
        public readonly int ShopKeepAmount = 2;
        public readonly int shopKeepBaseHP = 10;
        public readonly int shopKeepBaseAttack = 5;
        public readonly Tile shopKeepSprite = new Tile('¢', ConsoleColor.DarkYellow, ConsoleColor.Black);
        public readonly string shopKeepName = "Shop Keep";

        //Shop Price Settings
        public readonly int healthPotionCost = 8;
        public readonly int shieldRepairCost = 4;
        public readonly int ATKBuffCost = 12;

        //Slime Settings
        public readonly int slimeBaseHP = 1;
        public readonly int slimeBaseAttack = 1;
        public readonly int slimeXP = 1;
        public readonly int slimeGold = 1;
        public readonly Tile slimeSprite = new Tile('O', ConsoleColor.Cyan, ConsoleColor.Black);
        public readonly string slimeName = "Slime";

        //Goblin Settings
        public readonly int goblinBaseHP = 5;
        public readonly int goblinBaseAttack = 2;
        public readonly int goblinXP = 5;
        public readonly int goblinGold = 5;
        public readonly Tile goblinSprite = new Tile('X', ConsoleColor.DarkGreen, ConsoleColor.Black);
        public readonly string goblinName = "Goblin";

        //Kobold Settings
        public readonly int koboldBaseHP = 3;
        public readonly int koboldBaseAttack = 1;
        public readonly int koboldXP = 2;
        public readonly int koboldGold = 3;
        public readonly Tile koboldSprite = new Tile('X', ConsoleColor.DarkRed, ConsoleColor.Black);
        public readonly string koboldName = "Kobold";

        //Boss Settings
        public readonly int bossBaseHP = 150;
        public readonly int bossBaseAttack = 3;
        public readonly Tile bossSprite = new Tile('M', ConsoleColor.DarkRed, ConsoleColor.Black);
        public readonly string bossName = "Boss";

        //Item Settings
        public readonly int itemAmount = 50;
        public readonly int bossItemAmount = 10;
        public readonly int healAmount = 3;
        public readonly int ATKBuffAmount = 1;
        public readonly int shieldRepairAmount = 3;
        public readonly Tile healSprite = new Tile('+', ConsoleColor.Green, ConsoleColor.Black);
        public readonly Tile ATKSprite = new Tile('*', ConsoleColor.Red, ConsoleColor.Black);
        public readonly Tile ShieldRepairSprite = new Tile('#', ConsoleColor.Blue, ConsoleColor.Black);
        public readonly string healName = "Health Potion";
        public readonly string ATKBuffName = "ATK Buff";
        public readonly string ShieldRepairName = "Shield Repair";

        //Map Settings
        public readonly ConsoleColor mapColor = ConsoleColor.DarkGray;
        public readonly int mapHeight = 5;
        public readonly int mapWidth = 5;
        public readonly int roomHeight = 13;
        public readonly int roomWidth = 13;
        public readonly int BossRoomHeight = 15;
        public readonly int BossRoomWidth = 15;
        public readonly int BossFloor = 3;
        public readonly int RoomsPerCategory = 4;

        //Cam Settings
        public readonly int camSize = 15;

        //Render Settings
        public readonly int rendWidth = 35;
        public readonly int rendHeight = 50;
        public readonly ConsoleColor borderColor = ConsoleColor.White;
        public readonly ConsoleColor BGColor = ConsoleColor.Black;

        //HUD Settings
        public readonly int hudWidth = 33;
        public readonly int messageBoxHeight = 2;
        public readonly int statsHeight = 9;
        public readonly string playerStatsList = "Player|HP: 1/2|SHLD: 3/4|ATK: 5|LVL: 6|XP: 7/8|FLOOR: 9/0|GOLD: $";
        public readonly string enemyStatsList = "HP: 1|ATK: 2|XP: 3|GOLD: 4";
        public readonly string shopList = "1: HP Potion !g|2: Shield @g|3: ATK Buff #g|E key to leave";

        //Exit Settings
        public readonly Tile exitSprite = new Tile('¤', ConsoleColor.Yellow, ConsoleColor.Black);

        //Quest Strings
        public readonly string killEnemiesString = "kill 10 enemies";
        public readonly string loseShieldString = "lose your shield";
        public readonly string killBossString = "kill the boss";
        public readonly string buyFromShopString = "buy from shop";
        public readonly string pickUpItemsString = "get 10 items";

        //Quest Conditions
        public readonly int enemiesToKill = 10;
        public readonly int itemsToGet = 10;

        public Constants()
        {
            string[] lines = File.ReadAllLines("Data/Data.txt");

            playerBaseHP = GetInt(lines[0]);
            playerBaseShield = GetInt(lines[1]);
            playerBaseAttack = GetInt(lines[2]);
            playerXPThreshold = GetInt(lines[3]);
            playerSprite = GetTile(lines[4]);

            EnemySightRange = GetInt(lines[6]);
            EnemyAmount = GetInt(lines[7]);

            ShopKeepAmount = GetInt(lines[9]);
            shopKeepBaseHP = GetInt(lines[10]);
            shopKeepBaseAttack = GetInt(lines[11]);
            shopKeepSprite = GetTile(lines[12]);
            shopKeepName = GetString(lines[13]);

            healthPotionCost = GetInt(lines[15]);
            shieldRepairCost = GetInt(lines[16]);
            ATKBuffCost = GetInt(lines[17]);

            slimeBaseHP = GetInt(lines[19]);
            slimeBaseAttack = GetInt(lines[20]);
            slimeXP = GetInt(lines[21]);
            slimeGold = GetInt(lines[22]);
            slimeSprite = GetTile(lines[23]);
            slimeName = GetString(lines[24]);

            goblinBaseHP = GetInt(lines[26]);
            goblinBaseAttack = GetInt(lines[27]);
            goblinXP = GetInt(lines[28]);
            goblinGold = GetInt(lines[29]);
            goblinSprite = GetTile(lines[30]);
            goblinName = GetString(lines[31]);

            koboldBaseHP = GetInt(lines[33]);
            koboldBaseAttack = GetInt(lines[34]);
            koboldXP = GetInt(lines[35]);
            koboldGold = GetInt(lines[36]);
            koboldSprite = GetTile(lines[37]);
            koboldName = GetString(lines[38]);

            bossBaseHP = GetInt(lines[40]);
            bossBaseAttack = GetInt(lines[41]);
            bossSprite = GetTile(lines[42]);
            bossName = GetString(lines[43]);

            itemAmount = GetInt(lines[45]);
            bossItemAmount = GetInt(lines[45]);
            healAmount = GetInt(lines[47]);
            ATKBuffAmount = GetInt(lines[48]);
            shieldRepairAmount = GetInt(lines[49]);
            healSprite = GetTile(lines[50]);
            ATKSprite = GetTile(lines[51]);
            ShieldRepairSprite = GetTile(lines[52]);
            healName = GetString(lines[53]);
            ATKBuffName = GetString(lines[54]);
            ShieldRepairName = GetString(lines[55]);

        }

        private int GetInt(string line)
        {
            string[] result = line.Split(':');
            return Int32.Parse(result[1]);
        }

        private string GetString(string line)
        {
            string result = line.Split('"')[1];

            return result;
        }

        private Tile GetTile(string line)
        {
            string[] elements = line.Split('|');
            return new Tile(elements[1][0], GetColor(elements[2]), GetColor(elements[3]));
        }

        private ConsoleColor GetColor(string line)
        {
            string result = line;

            ConsoleColor color;

            switch (result)
            {
                case "<Black>":
                    color = ConsoleColor.Black;
                    break;
                case "<Blue>":
                    color = ConsoleColor.Blue;
                    break;
                case "<Cyan>":
                    color = ConsoleColor.Cyan;
                    break;
                case "<DarkBlue>":
                    color = ConsoleColor.DarkBlue;
                    break;
                case "<DarkCyan>":
                    color = ConsoleColor.DarkCyan;
                    break;
                case "<DarkGray>":
                    color = ConsoleColor.DarkGray;
                    break;
                case "<DarkGreen>":
                    color = ConsoleColor.DarkGreen;
                    break;
                case "<DarkMagenta>":
                    color = ConsoleColor.DarkMagenta;
                    break;
                case "<DarkRed>":
                    color = ConsoleColor.DarkRed;
                    break;
                case "<DarkYellow>":
                    color = ConsoleColor.DarkYellow;
                    break;
                case "<Gray>":
                    color = ConsoleColor.Gray;
                    break;
                case "<Green>":
                    color = ConsoleColor.Green;
                    break;
                case "<Magenta>":
                    color = ConsoleColor.Magenta;
                    break;
                case "<Red>":
                    color = ConsoleColor.Red;
                    break;
                case "<White>":
                    color = ConsoleColor.White;
                    break;
                case "<Yellow>":
                    color = ConsoleColor.Yellow;
                    break;
                default:
                    color = ConsoleColor.Yellow;
                    break;
            }
            return color;
        }

        
    }
}
