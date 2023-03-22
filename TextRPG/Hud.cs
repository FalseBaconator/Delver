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

        public char[,] hudArray = new char[Constants.messageBoxHeight + Constants.statsHeight + 2,Constants.hudWidth + 1];

        public Hud(Player player, EnemyManager enemyManager, GameManager manager)
        {
            this.player = player;
            this.enemyManager = enemyManager;
            this.manager = manager;
        }

        public void draw()
        {
            Console.ResetColor();



            //Set Message Box
            int messageIndex = 0;
            for (int i = 0; i <= Constants.messageBoxHeight; i++)
            {
                for (int j = 0; j <= Constants.hudWidth; j++)
                {
                    if(i == 0)
                    {
                        if (j == 0)
                            hudArray[i, j] = '╔';
                        else if (j == Constants.hudWidth)
                            hudArray[i, j] = '╗';
                        else
                            hudArray[i, j] = '═';
                    }else if (i == Constants.messageBoxHeight)
                    {
                        if (j == 0)
                            hudArray[i, j] = '╚';
                        else if (j == Constants.hudWidth)
                            hudArray[i, j] = '╝';
                        else
                            hudArray[i, j] = '═';
                    }else
                    {
                        if (j == 0 || j == Constants.hudWidth)
                            hudArray[i, j] = '║';
                        else if (message != null)
                        {
                            hudArray[i, j] = message[messageIndex];
                            messageIndex++;
                            if (messageIndex == message.Length)
                                message = null;
                        }
                        else
                        {
                            hudArray[i, j] = ' ';
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
                            hudArray[i, j] = '╔';
                        else if (j == Constants.hudWidth)
                            hudArray[i, j] = '╗';
                        else if (j == Constants.hudWidth / 2)
                            hudArray[i, j] = '╦';
                        else
                            hudArray[i, j] = '═';
                    }
                    else if (i == Constants.messageBoxHeight + 1 + Constants.statsHeight)
                    {
                        if (j == 0)
                            hudArray[i, j] = '╚';
                        else if (j == Constants.hudWidth)
                            hudArray[i, j] = '╝';
                        else if (j == Constants.hudWidth / 2)
                            hudArray[i, j] = '╩';
                        else
                            hudArray[i, j] = '═';
                    }
                    else
                    {
                        if (j == 0 || j == Constants.hudWidth || j == Constants.hudWidth / 2)
                            hudArray[i, j] = '║';
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
                                            hudArray[i, j] = player.GetHealth().ToString()[0];
                                            if (player.GetHealth() >= 10)
                                            {
                                                hudArray[i, j + 1] = player.GetHealth().ToString()[1];
                                                if(player.GetHealth() >= 100)
                                                    hudArray[i, j+2] = player.GetHealth().ToString()[2];
                                            }
                                            break;
                                        case 'Y':
                                            hudArray[i, j] = player.GetShield().ToString()[0];
                                            if (player.GetShield() >= 10)
                                            {
                                                hudArray[i, j + 1] = player.GetShield().ToString()[1];
                                                if (player.GetShield() >= 100)
                                                    hudArray[i, j + 2] = player.GetShield().ToString()[2];
                                            }
                                            break;
                                        case 'Z':
                                            hudArray[i, j] = player.GetATK().ToString()[0];
                                            if (player.GetATK() >= 10)
                                            {
                                                hudArray[i, j + 1] = player.GetATK().ToString()[1];
                                                if (player.GetATK() >= 100)
                                                    hudArray[i, j + 2] = player.GetATK().ToString()[2];
                                            }
                                            break;
                                        case '$':
                                            hudArray[i, j] = manager.getFloor().ToString()[0];
                                            if (manager.getFloor() >= 10)
                                                hudArray[i, j + 1] = manager.getFloor().ToString()[1];
                                            break;
                                        case '^':
                                            hudArray[i, j] = Constants.BossFloor.ToString()[0];
                                            break;
                                        default:
                                            hudArray[i, j] = Constants.playerStatsList[playerStatIndex];
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
                                                hudArray[i, j] = enemy.GetHealth().ToString()[0];
                                                if (enemy.GetHealth() >= 10)
                                                {
                                                    hudArray[i, j + 1] = enemy.GetHealth().ToString()[1];
                                                    if (enemy.GetHealth() >= 100)
                                                        hudArray[i, j + 2] = enemy.GetHealth().ToString()[2];
                                                    else
                                                        hudArray[i, j + 2] = ' ';
                                                }
                                                else
                                                {
                                                    hudArray[i, j + 1] = ' ';
                                                    hudArray[i, j + 2] = ' ';
                                                }
                                                break;
                                            case 'Z':
                                                hudArray[i, j] = enemy.GetATK().ToString()[0];
                                                if (enemy.GetATK() >= 10)
                                                {
                                                    hudArray[i, j + 1] = enemy.GetATK().ToString()[1];
                                                    if (enemy.GetATK() >= 100)
                                                        hudArray[i, j + 2] = enemy.GetATK().ToString()[2];
                                                    else
                                                        hudArray[i, j + 2] = ' ';
                                                }
                                                else
                                                {
                                                    hudArray[i, j + 1] = ' ';
                                                    hudArray[i, j + 2] = ' ';
                                                }
                                                break;
                                            default:
                                                hudArray[i, j] = enemyStatString[enemyStatIndex];
                                                break;
                                        }
                                        enemyStatIndex++;
                                    }
                                }
                            }
                        }
                        else
                        {
                            hudArray[i, j] = ' ';
                        }
                    }
                }
            }
        }

        public void SetMessage(string message)
        {
            this.message = message;
        }


    }
}
