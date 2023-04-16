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
        private string message;
        private GameManager manager;

        public Tile[,] hudArray = new Tile[Constants.messageBoxHeight + Constants.statsHeight + 2,Constants.hudWidth + 1];

        public Hud(Player player, EnemyManager enemyManager, ItemManager itemManager, GameManager manager)
        {
            this.player = player;
            this.player.SetHud(this);
            this.enemyManager = enemyManager;
            this.enemyManager.SetHud(this);
            itemManager.SetHud(this);
            this.manager = manager;
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
            for (int i = 0; i <= Constants.messageBoxHeight; i++)
            {
                for (int j = 0; j <= Constants.hudWidth; j++)
                {
                    if(i == 0)
                    {
                        if (j == 0)
                            hudArray[i, j] = new Tile('╔', Constants.borderColor, Constants.BGColor);
                        else if (j == Constants.hudWidth)
                            hudArray[i, j] = new Tile('╗', Constants.borderColor, Constants.BGColor);
                        else
                            hudArray[i, j] = new Tile('═', Constants.borderColor, Constants.BGColor);
                    }else if (i == Constants.messageBoxHeight)
                    {
                        if (j == 0)
                            hudArray[i, j] = new Tile('╚', Constants.borderColor, Constants.BGColor);
                        else if (j == Constants.hudWidth)
                            hudArray[i, j] = new Tile('╝', Constants.borderColor, Constants.BGColor);
                        else
                            hudArray[i, j] = new Tile('═', Constants.borderColor, Constants.BGColor);
                    }else
                    {
                        if (j == 0 || j == Constants.hudWidth)
                            hudArray[i, j] = new Tile('║', Constants.borderColor, Constants.BGColor);
                        else if (message != null)
                        {
                            hudArray[i, j] = new Tile(message[messageIndex], Constants.borderColor, Constants.BGColor);
                            messageIndex++;
                            if (messageIndex == message.Length)
                                message = null;
                        }
                        else
                        {
                            hudArray[i, j] = new Tile(' ', Constants.borderColor, Constants.BGColor);
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
            if(enemy != null)
                enemyStatString = enemy.GetName() + "|" + Constants.enemyStatsList;
            for (int i = Constants.messageBoxHeight + 1; i <= Constants.messageBoxHeight + 1 + Constants.statsHeight; i++)
            {
                playerNextLine = false;
                enemyNextLine = false;
                for (int j = 0; j <= Constants.hudWidth; j++)
                {
                    if (i == Constants.messageBoxHeight + 1)
                    {
                        if (j == 0)
                            hudArray[i, j] = new Tile('╔', Constants.borderColor, Constants.BGColor);
                        else if (j == Constants.hudWidth)
                            hudArray[i, j] = new Tile('╗', Constants.borderColor, Constants.BGColor);
                        else if (j == Constants.hudWidth / 2)
                            hudArray[i, j] = new Tile('╦', Constants.borderColor, Constants.BGColor);
                        else
                            hudArray[i, j] = new Tile('═', Constants.borderColor, Constants.BGColor);
                    }
                    else if (i == Constants.messageBoxHeight + 1 + Constants.statsHeight)
                    {
                        if (j == 0)
                            hudArray[i, j] = new Tile('╚', Constants.borderColor, Constants.BGColor);
                        else if (j == Constants.hudWidth)
                            hudArray[i, j] = new Tile('╝', Constants.borderColor, Constants.BGColor);
                        else if (j == Constants.hudWidth / 2)
                            hudArray[i, j] = new Tile('╩', Constants.borderColor, Constants.BGColor);
                        else
                            hudArray[i, j] = new Tile('═', Constants.borderColor, Constants.BGColor);
                    }
                    else
                    {
                        if (j == 0 || j == Constants.hudWidth || j == Constants.hudWidth / 2)
                            hudArray[i, j] = new Tile('║', Constants.borderColor, Constants.BGColor);
                        else if (j < Constants.hudWidth / 2 && playerNextLine != true)
                        {
                            if (playerStatIndex < Constants.playerStatsList.Length)
                            {
                                if (Constants.playerStatsList[playerStatIndex] == '|')
                                {
                                    //hudArray[i, j] = ' ';
                                    playerNextLine = true;
                                    playerStatIndex++;
                                }
                                else
                                {
                                    switch (Constants.playerStatsList[playerStatIndex])
                                    {
                                        case 'X':
                                            hudArray[i, j] = new Tile(player.GetHealth().ToString()[0], Constants.borderColor, Constants.BGColor);
                                            if (player.GetHealth() >= 10)
                                            {
                                                hudArray[i, j + 1] = new Tile(player.GetHealth().ToString()[1], Constants.borderColor, Constants.BGColor);
                                                if(player.GetHealth() >= 100)
                                                    hudArray[i, j+2] = new Tile(player.GetHealth().ToString()[2], Constants.borderColor, Constants.BGColor);
                                            }
                                            break;
                                        case 'Y':
                                            hudArray[i, j] = new Tile(player.GetShield().ToString()[0], Constants.borderColor, Constants.BGColor);
                                            if (player.GetShield() >= 10)
                                            {
                                                hudArray[i, j + 1] = new Tile(player.GetShield().ToString()[1], Constants.borderColor, Constants.BGColor);
                                                if (player.GetShield() >= 100)
                                                    hudArray[i, j + 2] = new Tile(player.GetShield().ToString()[2], Constants.borderColor, Constants.BGColor);
                                            }
                                            break;
                                        case 'Z':
                                            hudArray[i, j] = new Tile(player.GetATK().ToString()[0], Constants.borderColor, Constants.BGColor);
                                            if (player.GetATK() >= 10)
                                            {
                                                hudArray[i, j + 1] = new Tile(player.GetATK().ToString()[1], Constants.borderColor, Constants.BGColor);
                                                if (player.GetATK() >= 100)
                                                    hudArray[i, j + 2] = new Tile(player.GetATK().ToString()[2], Constants.borderColor, Constants.BGColor);
                                            }
                                            break;
                                        case '$':
                                            hudArray[i, j] = new Tile(Globals.currentFloor.ToString()[0], Constants.borderColor, Constants.BGColor);
                                            if (Globals.currentFloor >= 10)
                                                hudArray[i, j + 1] = new Tile(Globals.currentFloor.ToString()[1], Constants.borderColor, Constants.BGColor);
                                            break;
                                        case '^':
                                            hudArray[i, j] = new Tile(Constants.BossFloor.ToString()[0], Constants.borderColor, Constants.BGColor);
                                            break;
                                        default:
                                            hudArray[i, j] = new Tile(Constants.playerStatsList[playerStatIndex], Constants.borderColor, Constants.BGColor);
                                            break;
                                    }
                                    playerStatIndex++;
                                    
                                }
                            }
                        }
                        else if (enemy != null)
                        {
                            if (j > Constants.hudWidth / 2 && j < Constants.hudWidth && enemyNextLine != true)
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
                                            case 'X':
                                                hudArray[i, j] = new Tile(enemy.GetHealth().ToString()[0], Constants.borderColor, Constants.BGColor);
                                                if (enemy.GetHealth() >= 10)
                                                {
                                                    hudArray[i, j + 1] = new Tile(enemy.GetHealth().ToString()[1], Constants.borderColor, Constants.BGColor);
                                                    if (enemy.GetHealth() >= 100)
                                                        hudArray[i, j + 2] = new Tile(enemy.GetHealth().ToString()[2], Constants.borderColor, Constants.BGColor);
                                                    else
                                                        hudArray[i, j + 2] = new Tile(' ', Constants.borderColor, Constants.BGColor);
                                                }
                                                else
                                                {
                                                    hudArray[i, j + 1] = new Tile(' ', Constants.borderColor, Constants.BGColor);
                                                    hudArray[i, j + 2] = new Tile(' ', Constants.borderColor, Constants.BGColor);
                                                }
                                                break;
                                            case 'Z':
                                                hudArray[i, j] = new Tile(enemy.GetATK().ToString()[0], Constants.borderColor, Constants.BGColor);
                                                if (enemy.GetATK() >= 10)
                                                {
                                                    hudArray[i, j + 1] = new Tile(enemy.GetATK().ToString()[1], Constants.borderColor, Constants.BGColor);
                                                    if (enemy.GetATK() >= 100)
                                                        hudArray[i, j + 2] = new Tile(enemy.GetATK().ToString()[2], Constants.borderColor, Constants.BGColor);
                                                    else
                                                        hudArray[i, j + 2] = new Tile(' ', Constants.borderColor, Constants.BGColor);
                                                }
                                                else
                                                {
                                                    hudArray[i, j + 1] = new Tile(' ', Constants.borderColor, Constants.BGColor);
                                                    hudArray[i, j + 2] = new Tile(' ', Constants.borderColor, Constants.BGColor);
                                                }
                                                break;
                                            default:
                                                hudArray[i, j] = new Tile(enemyStatString[enemyStatIndex], Constants.borderColor, Constants.BGColor);
                                                break;
                                        }
                                        enemyStatIndex++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            hudArray[i, j] = new Tile(' ', Constants.borderColor, Constants.BGColor);
                        }
                    }
                }
            }
        }

        public void SetMessage(string message)
        {
            this.message = message;
        }

        public string GetMessage()
        {
            return message;
        }

    }
}
