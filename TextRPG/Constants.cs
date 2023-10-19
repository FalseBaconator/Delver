using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public readonly int playerBaseHP;
        public readonly int playerBaseShield;
        public readonly int playerBaseAttack;
        public readonly int playerXPThreshold;
        public readonly Tile playerSprite;

        //Enemy Settings
        public readonly int EnemySightRange;
        public readonly int EnemyAmount;

        //ShopKeep Settings
        public readonly int ShopKeepAmount;
        public readonly int shopKeepBaseHP;
        public readonly int shopKeepBaseAttack;
        public readonly Tile shopKeepSprite;
        public readonly string shopKeepName;

        //Shop Price Settings
        public readonly int healthPotionCost;
        public readonly int shieldRepairCost;
        public readonly int ATKBuffCost;

        //Slime Settings
        public readonly int slimeBaseHP;
        public readonly int slimeBaseAttack;
        public readonly int slimeXP;
        public readonly int slimeGold;
        public readonly Tile slimeSprite;
        public readonly string slimeName;

        //Goblin Settings
        public readonly int goblinBaseHP;
        public readonly int goblinBaseAttack;
        public readonly int goblinXP;
        public readonly int goblinGold;
        public readonly Tile goblinSprite;
        public readonly string goblinName;

        //Kobold Settings
        public readonly int koboldBaseHP;
        public readonly int koboldBaseAttack;
        public readonly int koboldXP;
        public readonly int koboldGold;
        public readonly Tile koboldSprite;
        public readonly string koboldName;

        //Boss Settings
        public readonly int bossBaseHP;
        public readonly int bossBaseAttack;
        public readonly Tile bossSprite;
        public readonly string bossName;

        //Item Settings
        public readonly int itemAmount;
        public readonly int bossItemAmount;
        public readonly int healAmount;
        public readonly int ATKBuffAmount;
        public readonly int shieldRepairAmount;
        public readonly Tile healSprite;
        public readonly Tile ATKSprite;
        public readonly Tile ShieldRepairSprite;
        public readonly string healName;
        public readonly string ATKBuffName;
        public readonly string ShieldRepairName;

        //Map Settings
        public readonly ConsoleColor mapColor;
        public readonly int mapHeight;
        public readonly int mapWidth;
        public readonly int roomHeight;
        public readonly int roomWidth;
        public readonly int BossRoomHeight;
        public readonly int BossRoomWidth;
        public readonly int BossFloor;
        public readonly int RoomsPerCategory;

        //Cam Settings
        public readonly int camSize;

        //Render Settings
        public readonly int rendWidth;
        public readonly int rendHeight;
        public readonly ConsoleColor borderColor;
        public readonly ConsoleColor BGColor;

        //HUD Settings
        public readonly int hudWidth;
        public readonly int messageBoxHeight;
        public readonly int statsHeight;
        public readonly string playerStatsList;
        public readonly string enemyStatsList;
        public readonly string shopList;

        //Exit Settings
        public readonly Tile exitSprite;

        //Quest Strings
        public readonly string killEnemiesString;
        public readonly string loseShieldString;
        public readonly string killBossString;
        public readonly string buyFromShopString;
        public readonly string pickUpItemsString;

        //Quest Conditions
        public readonly int enemiesToKill;
        public readonly int itemsToGet;

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

            itemAmount = GetInt(lines[19]);
            bossItemAmount = GetInt(lines[20]);
            healAmount = GetInt(lines[21]);
            ATKBuffAmount = GetInt(lines[22]);
            shieldRepairAmount = GetInt(lines[23]);
            healSprite = GetTile(lines[24]);
            ATKSprite = GetTile(lines[25]);
            ShieldRepairSprite = GetTile(lines[26]);
            healName = GetString(lines[27]);
            ATKBuffName = GetString(lines[28]);
            ShieldRepairName = GetString(lines[29]);

            mapColor = GetColor(lines[31]);
            mapHeight = GetInt(lines[32]);
            mapWidth = GetInt(lines[33]);
            roomHeight = GetInt(lines[34]);
            roomWidth = GetInt(lines[35]);
            BossRoomHeight = GetInt(lines[36]);
            BossRoomWidth = GetInt(lines[37]);
            BossFloor = GetInt(lines[38]);
            RoomsPerCategory = GetInt(lines[39]);

            camSize = GetInt(lines[41]);

            rendWidth = GetInt(lines[43]);
            rendHeight = GetInt(lines[44]);
            borderColor = GetColor(lines[45]);
            BGColor = GetColor(lines[46]);

            hudWidth = GetInt(lines[48]);
            messageBoxHeight = GetInt(lines[49]);
            statsHeight = GetInt(lines[50]);
            playerStatsList = GetString(lines[51]);
            enemyStatsList = GetString(lines[52]);
            shopList = GetString(lines[53]);

            exitSprite = GetTile(lines[55]);

            killEnemiesString = GetString(lines[57]);
            loseShieldString = GetString(lines[58]);
            killBossString = GetString(lines[59]);
            buyFromShopString = GetString(lines[60]);
            pickUpItemsString = GetString(lines[61]);

            enemiesToKill = GetInt(lines[63]);
            itemsToGet = GetInt(lines[64]);

        }

        public static int GetInt(string line)
        {
            string[] result = line.Split(':');
            return Int32.Parse(result[1]);
        }

        public static string GetString(string line)
        {
            string result = line.Split('"')[1];

            return result;
        }

        public static Tile GetTile(string line)
        {
            string[] elements = line.Split('|');
            return new Tile(elements[1][0], GetColor(elements[2]), GetColor(elements[3]));
        }

        public static ConsoleColor GetColor(string line)
        {
            string result = line.Split('!')[1];

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
