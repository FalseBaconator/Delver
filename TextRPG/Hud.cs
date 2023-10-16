using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Hud
    {
        private Player player;
        private EnemyManager enemyManager;
        private Enemy enemy;
        private string message = null;
        private GameManager manager;
        private Shop shop;
        private bool isMessageImportant;

        public Tile[,] hudArray = new Tile[GameManager.constants.messageBoxHeight + GameManager.constants.statsHeight + 2,GameManager.constants.hudWidth + 1];

        public Hud(Player player, EnemyManager enemyManager, ItemManager itemManager, GameManager manager, Shop shop, Exit exit)
        {
            this.player = player;
            this.player.SetHud(this);
            this.enemyManager = enemyManager;
            this.enemyManager.SetHud(this);
            this.shop = shop;
            this.shop.SetHud(this);
            itemManager.SetHud(this);
            this.manager = manager;
            exit.SetHud(this);
        }

        public void draw()
        {
            Console.ResetColor();

            for (int i = 0; i < hudArray.GetLength(0); i++)
            {
                for (int j = 0; j < hudArray.GetLength(1); j++)
                {
                    hudArray[i, j] = new Tile(' ', ConsoleColor.White, ConsoleColor.Black);
                }
            }


            //Set Message Box
            int messageIndex = 0;
            for (int i = 0; i <= GameManager.constants.messageBoxHeight; i++)
            {
                for (int j = 0; j <= GameManager.constants.hudWidth; j++)
                {
                    if(i == 0)
                    {
                        if (j == 0)
                            hudArray[i, j] = new Tile('╔', GameManager.constants.borderColor, GameManager.constants.BGColor);
                        else if (j == GameManager.constants.hudWidth)
                            hudArray[i, j] = new Tile('╗', GameManager.constants.borderColor, GameManager.constants.BGColor);
                        else
                            hudArray[i, j] = new Tile('═', GameManager.constants.borderColor, GameManager.constants.BGColor);
                    }else if (i == GameManager.constants.messageBoxHeight)
                    {
                        if (j == 0)
                            hudArray[i, j] = new Tile('╚', GameManager.constants.borderColor, GameManager.constants.BGColor);
                        else if (j == GameManager.constants.hudWidth)
                            hudArray[i, j] = new Tile('╝', GameManager.constants.borderColor, GameManager.constants.BGColor);
                        else
                            hudArray[i, j] = new Tile('═', GameManager.constants.borderColor, GameManager.constants.BGColor);
                    }else
                    {
                        if (j == 0 || j == GameManager.constants.hudWidth)
                            hudArray[i, j] = new Tile('║', GameManager.constants.borderColor, GameManager.constants.BGColor);
                        else if (message != null)
                        {
                            hudArray[i, j] = new Tile(message[messageIndex], GameManager.constants.borderColor, GameManager.constants.BGColor);
                            messageIndex++;
                            if (messageIndex == message.Length)
                                message = null;
                        }
                        else
                        {
                            hudArray[i, j] = new Tile(' ', GameManager.constants.borderColor, GameManager.constants.BGColor);
                        }
                    }
                }
            }

            //Set Stat Boxes
            int playerStatIndex = 0;
            bool playerNextLine = false;
            enemy = enemyManager.GetLastAttacked();
            int enemyStatIndex = 0;
            bool enemyNextLine = false;
            string enemyStatString = " ";
            int shopStatIndex = 0;
            bool shopNextLine = false;
            int playerTextOffset = 0;
            int enemyTextOffset = 0;
            int shopTextOffset = 0;
            if(enemy != null)
                enemyStatString = enemy.GetName() + "|" + GameManager.constants.enemyStatsList;
            for (int i = GameManager.constants.messageBoxHeight + 1; i <= GameManager.constants.messageBoxHeight + 1 + GameManager.constants.statsHeight; i++)
            {
                playerNextLine = false;
                enemyNextLine = false;
                shopNextLine = false;
                playerTextOffset = 0;
                shopTextOffset = 0;
                for (int j = 0; j <= GameManager.constants.hudWidth; j++)
                {
                    if (i == GameManager.constants.messageBoxHeight + 1)
                    {
                        if (j == 0)
                            hudArray[i, j] = new Tile('╔', GameManager.constants.borderColor, GameManager.constants.BGColor);
                        else if (j == GameManager.constants.hudWidth)
                            hudArray[i, j] = new Tile('╗', GameManager.constants.borderColor, GameManager.constants.BGColor);
                        else if (j == GameManager.constants.hudWidth / 2)
                            hudArray[i, j] = new Tile('╦', GameManager.constants.borderColor, GameManager.constants.BGColor);
                        else
                            hudArray[i, j] = new Tile('═', GameManager.constants.borderColor, GameManager.constants.BGColor);
                    }
                    else if (i == GameManager.constants.messageBoxHeight + 1 + GameManager.constants.statsHeight)
                    {
                        if (j == 0)
                            hudArray[i, j] = new Tile('╚', GameManager.constants.borderColor, GameManager.constants.BGColor);
                        else if (j == GameManager.constants.hudWidth)
                            hudArray[i, j] = new Tile('╝', GameManager.constants.borderColor, GameManager.constants.BGColor);
                        else if (j == GameManager.constants.hudWidth / 2)
                            hudArray[i, j] = new Tile('╩', GameManager.constants.borderColor, GameManager.constants.BGColor);
                        else
                            hudArray[i, j] = new Tile('═', GameManager.constants.borderColor, GameManager.constants.BGColor);
                    }
                    else
                    {
                        if (j == 0 || j == GameManager.constants.hudWidth || j == GameManager.constants.hudWidth / 2)
                            hudArray[i, j] = new Tile('║', GameManager.constants.borderColor, GameManager.constants.BGColor);
                        else if (j < GameManager.constants.hudWidth / 2 && playerNextLine != true)
                        {
                            if (playerStatIndex < GameManager.constants.playerStatsList.Length)
                            {
                                if (GameManager.constants.playerStatsList[playerStatIndex] == '|')
                                {
                                    //hudArray[i, j] = ' ';
                                    playerNextLine = true;
                                    playerStatIndex++;
                                }
                                else
                                {
                                    switch (GameManager.constants.playerStatsList[playerStatIndex])
                                    {
                                        case '1':
                                            hudArray[i, j + playerTextOffset] = new Tile(player.GetHealth().ToString()[0], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                            if (player.GetHealth() >= 10)
                                            {
                                                playerTextOffset++;
                                                hudArray[i, j + playerTextOffset] = new Tile(player.GetHealth().ToString()[1], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                if(player.GetHealth() >= 100)
                                                {
                                                    playerTextOffset++;
                                                    hudArray[i, j + playerTextOffset] = new Tile(player.GetHealth().ToString()[2], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                }
                                            }
                                            break;
                                        case '2':
                                            hudArray[i, j + playerTextOffset] = new Tile(player.GetMaxHealth().ToString()[0], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                            if (player.GetMaxHealth() >= 10)
                                            {
                                                playerTextOffset++;
                                                hudArray[i, j + playerTextOffset] = new Tile(player.GetMaxHealth().ToString()[1], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                if (player.GetMaxHealth() >= 100)
                                                {
                                                    playerTextOffset++;
                                                    hudArray[i, j + playerTextOffset] = new Tile(player.GetMaxHealth().ToString()[2], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                }
                                            }
                                            break;
                                        case '3':
                                            hudArray[i, j + playerTextOffset] = new Tile(player.GetShield().ToString()[0], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                            if (player.GetShield() >= 10)
                                            {
                                                playerTextOffset++;
                                                hudArray[i, j + playerTextOffset] = new Tile(player.GetShield().ToString()[1], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                if (player.GetShield() >= 100)
                                                {
                                                    playerTextOffset++;
                                                    hudArray[i, j + playerTextOffset] = new Tile(player.GetShield().ToString()[2], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                }
                                            }
                                            break;
                                        case '4':
                                            hudArray[i, j + playerTextOffset] = new Tile(player.GetMaxShield().ToString()[0], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                            if (player.GetMaxShield() >= 10)
                                            {
                                                playerTextOffset++;
                                                hudArray[i, j + playerTextOffset] = new Tile(player.GetMaxShield().ToString()[1], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                if (player.GetMaxShield() >= 100)
                                                {
                                                    playerTextOffset++;
                                                    hudArray[i, j + playerTextOffset] = new Tile(player.GetMaxShield().ToString()[2], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                }
                                            }
                                            break;
                                        case '5':
                                            hudArray[i, j + playerTextOffset] = new Tile(player.GetATK().ToString()[0], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                            if (player.GetATK() >= 10)
                                            {
                                                playerTextOffset++;
                                                hudArray[i, j + playerTextOffset] = new Tile(player.GetATK().ToString()[1], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                if (player.GetATK() >= 100)
                                                {
                                                    playerTextOffset++;
                                                    hudArray[i, j + playerTextOffset] = new Tile(player.GetATK().ToString()[2], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                }
                                            }
                                            break;
                                        case '6':
                                            hudArray[i, j + playerTextOffset] = new Tile(player.GetLevel().ToString()[0], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                            if (player.GetLevel() >= 10)
                                            {
                                                playerTextOffset++;
                                                hudArray[i, j + playerTextOffset] = new Tile(player.GetLevel().ToString()[1], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                if (player.GetLevel() >= 100)
                                                {
                                                    playerTextOffset++;
                                                    hudArray[i, j + playerTextOffset] = new Tile(player.GetLevel().ToString()[2], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                }
                                            }
                                            break;
                                        case '7':
                                            hudArray[i, j + playerTextOffset] = new Tile(player.GetXP().ToString()[0], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                            if (player.GetXP() >= 10)
                                            {
                                                playerTextOffset++;
                                                hudArray[i, j + playerTextOffset] = new Tile(player.GetXP().ToString()[1], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                if (player.GetXP() >= 100)
                                                {
                                                    playerTextOffset++;
                                                    hudArray[i, j + playerTextOffset] = new Tile(player.GetXP().ToString()[2], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                }
                                            }
                                            break;
                                        case '8':
                                            hudArray[i, j + playerTextOffset] = new Tile(GameManager.constants.playerXPThreshold.ToString()[0], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                            if (GameManager.constants.playerXPThreshold >= 10)
                                            {
                                                playerTextOffset++;
                                                hudArray[i, j + playerTextOffset] = new Tile(GameManager.constants.playerXPThreshold.ToString()[1], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                if (GameManager.constants.playerXPThreshold >= 100)
                                                {
                                                    playerTextOffset++;
                                                    hudArray[i, j + playerTextOffset] = new Tile(GameManager.constants.playerXPThreshold.ToString()[2], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                }
                                            }
                                            break;
                                        case '9':
                                            hudArray[i, j + playerTextOffset] = new Tile(Globals.currentFloor.ToString()[0], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                            if (Globals.currentFloor >= 10)
                                            {
                                                playerTextOffset++;
                                                hudArray[i, j + playerTextOffset] = new Tile(Globals.currentFloor.ToString()[1], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                            }
                                            break;
                                        case '0':
                                            hudArray[i, j + playerTextOffset] = new Tile(GameManager.constants.BossFloor.ToString()[0], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                            break;
                                        case '$':
                                            hudArray[i, j + playerTextOffset] = new Tile(player.GetGold().ToString()[0], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                            if (player.GetGold() >= 10)
                                            {
                                                playerTextOffset++;
                                                hudArray[i, j + playerTextOffset] = new Tile(player.GetGold().ToString()[1], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                if (player.GetGold() >= 100)
                                                {
                                                    playerTextOffset++;
                                                    hudArray[i, j + playerTextOffset] = new Tile(player.GetGold().ToString()[2], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                }
                                            }
                                            break;
                                        default:
                                            hudArray[i, j + playerTextOffset] = new Tile(GameManager.constants.playerStatsList[playerStatIndex], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                            break;
                                    }
                                    playerStatIndex++;
                                    
                                }
                            }
                        }
                        else if (enemy != null && !Globals.shopping)
                        {
                            if (j > GameManager.constants.hudWidth / 2 && j < GameManager.constants.hudWidth && enemyNextLine != true)
                            {
                                if (enemyStatIndex < enemyStatString.Length)
                                {
                                    if (enemyStatString[enemyStatIndex] == '|')
                                    {
                                        //hudArray[i, j] = ' ';
                                        enemyNextLine = true;
                                        enemyStatIndex++;
                                    }
                                    else
                                    {
                                        switch (enemyStatString[enemyStatIndex])
                                        {
                                            case '1':
                                                hudArray[i, j] = new Tile(enemy.GetHealth().ToString()[0], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                if (enemy.GetHealth() >= 10)
                                                {
                                                    hudArray[i, j + 1] = new Tile(enemy.GetHealth().ToString()[1], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                    if (enemy.GetHealth() >= 100)
                                                        hudArray[i, j + 2] = new Tile(enemy.GetHealth().ToString()[2], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                    else
                                                        hudArray[i, j + 2] = new Tile(' ', GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                }
                                                else
                                                {
                                                    hudArray[i, j + 1] = new Tile(' ', GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                    hudArray[i, j + 2] = new Tile(' ', GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                }
                                                break;
                                            case '2':
                                                hudArray[i, j] = new Tile(enemy.GetATK().ToString()[0], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                if (enemy.GetATK() >= 10)
                                                {
                                                    hudArray[i, j + 1] = new Tile(enemy.GetATK().ToString()[1], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                    if (enemy.GetATK() >= 100)
                                                        hudArray[i, j + 2] = new Tile(enemy.GetATK().ToString()[2], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                    else
                                                        hudArray[i, j + 2] = new Tile(' ', GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                }
                                                else
                                                {
                                                    hudArray[i, j + 1] = new Tile(' ', GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                    hudArray[i, j + 2] = new Tile(' ', GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                }
                                                break;
                                            case '3':
                                                hudArray[i, j] = new Tile(enemy.GetXP().ToString()[0], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                if (enemy.GetXP() >= 10)
                                                {
                                                    hudArray[i, j + 1] = new Tile(enemy.GetXP().ToString()[1], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                    if (enemy.GetXP() >= 100)
                                                        hudArray[i, j + 2] = new Tile(enemy.GetXP().ToString()[2], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                    else
                                                        hudArray[i, j + 2] = new Tile(' ', GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                }
                                                else
                                                {
                                                    hudArray[i, j + 1] = new Tile(' ', GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                    hudArray[i, j + 2] = new Tile(' ', GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                }
                                                break;
                                            case '4':
                                                hudArray[i, j] = new Tile(enemy.GetGold().ToString()[0], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                if (enemy.GetGold() >= 10)
                                                {
                                                    hudArray[i, j + 1] = new Tile(enemy.GetGold().ToString()[1], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                    if (enemy.GetGold() >= 100)
                                                        hudArray[i, j + 2] = new Tile(enemy.GetGold().ToString()[2], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                    else
                                                        hudArray[i, j + 2] = new Tile(' ', GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                }
                                                else
                                                {
                                                    hudArray[i, j + 1] = new Tile(' ', GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                    hudArray[i, j + 2] = new Tile(' ', GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                }
                                                break;
                                            default:
                                                hudArray[i, j] = new Tile(enemyStatString[enemyStatIndex], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                break;
                                        }
                                        enemyStatIndex++;
                                    }
                                }
                            }
                        }
                        else if (Globals.shopping)
                        {
                            if (j > GameManager.constants.hudWidth / 2 && j < GameManager.constants.hudWidth && shopNextLine != true)
                            {
                                if (shopStatIndex < GameManager.constants.shopList.Length)
                                {
                                    if (GameManager.constants.shopList[shopStatIndex] == '|')
                                    {
                                        //hudArray[i, j] = ' ';
                                        shopNextLine = true;
                                        shopStatIndex++;
                                    }
                                    else
                                    {
                                        switch (GameManager.constants.shopList[shopStatIndex])
                                        {
                                            case '!':
                                                hudArray[i, j + shopTextOffset] = new Tile(GameManager.constants.healthPotionCost.ToString()[0], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                if (GameManager.constants.healthPotionCost >= 10)
                                                {
                                                    shopTextOffset++;
                                                    hudArray[i, j + shopTextOffset] = new Tile(GameManager.constants.healthPotionCost.ToString()[1], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                    if (GameManager.constants.healthPotionCost >= 100)
                                                    {
                                                        shopTextOffset++;
                                                        hudArray[i, j + shopTextOffset] = new Tile(GameManager.constants.healthPotionCost.ToString()[2], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                    }
                                                }
                                                break;
                                            case '@':
                                                hudArray[i, j + shopTextOffset] = new Tile(GameManager.constants.shieldRepairCost.ToString()[0], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                if (GameManager.constants.shieldRepairCost >= 10)
                                                {
                                                    shopTextOffset++;
                                                    hudArray[i, j + shopTextOffset] = new Tile(GameManager.constants.shieldRepairCost.ToString()[1], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                    if (GameManager.constants.shieldRepairCost >= 100)
                                                    {
                                                        shopTextOffset++;
                                                        hudArray[i, j + shopTextOffset] = new Tile(GameManager.constants.shieldRepairCost.ToString()[2], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                    }
                                                }
                                                break;
                                            case '#':
                                                hudArray[i, j + shopTextOffset] = new Tile(GameManager.constants.ATKBuffCost.ToString()[0], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                if (GameManager.constants.ATKBuffCost >= 10)
                                                {
                                                    shopTextOffset++;
                                                    hudArray[i, j + shopTextOffset] = new Tile(GameManager.constants.ATKBuffCost.ToString()[1], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                    if (GameManager.constants.ATKBuffCost >= 100)
                                                    {
                                                        shopTextOffset++;
                                                        hudArray[i, j + shopTextOffset] = new Tile(GameManager.constants.ATKBuffCost.ToString()[2], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                    }
                                                }
                                                break;
                                            default:
                                                hudArray[i, j + shopTextOffset] = new Tile(GameManager.constants.shopList[shopStatIndex], GameManager.constants.borderColor, GameManager.constants.BGColor);
                                                break;
                                        }
                                        shopStatIndex++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            hudArray[i, j] = new Tile(' ', GameManager.constants.borderColor, GameManager.constants.BGColor);
                        }
                    }
                }
            }
            isMessageImportant = false;
        }

        public void SetMessage(string message, bool important = false)
        {
            if (isMessageImportant) return;
            this.message = message;
            isMessageImportant = important;
        }

        public string GetMessage()
        {
            return message;
        }
    }
}
