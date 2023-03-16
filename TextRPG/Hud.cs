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
        private int x;
        private int y;

        public char[,] hudArray = new char[Constants.messageBoxHeight + Constants.statsHeight + 2,Constants.hudWidth + 1];

        public Hud(Player player, EnemyManager enemyManager, int x, int y)
        {
            this.player = player;
            this.enemyManager = enemyManager;
            this.x = x;
            this.y = y;
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
                                    hudArray[i, j] = ' ';
                                    playerNextLine = true;
                                    playerStatIndex++;
                                }
                                else
                                {
                                    hudArray[i, j] = Constants.playerStatsList[playerStatIndex];
                                    playerStatIndex++;
                                }
                            }
                        }
                        else if (enemy != null)
                        {
                            if (j < Constants.hudWidth / 2 && enemyNextLine != true)
                            {
                                if (enemyStatIndex < enemyStatString.Length)
                                {
                                    if (enemyStatString[enemyStatIndex] == '|')
                                    {
                                        hudArray[i, j] = ' ';
                                        enemyNextLine = true;
                                        enemyStatIndex++;
                                    }
                                    else
                                    {
                                        hudArray[i, j] = enemyStatString[enemyStatIndex];
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



            /*
            Console.ResetColor();                                           //  Setup
                                                                            //
            Console.SetCursorPosition(x, y);                                //  //
            for (int i = 0; i < 10; i++)                                    //  //
            {                                                               //  //  Erase
                Console.WriteLine("                                   ");   //  //
            }                                                               //  //
                                                                            //
                                                                            //
            Console.SetCursorPosition(x, y);                                //  //
            Console.WriteLine("╔═════════════════════════════════╗");       //  //  Draw Message Box
            Console.WriteLine("║                                 ║");       //  //
            Console.WriteLine("╚═════════════════════════════════╝");       //  //
                                                                            //
                                                                            //
            Console.SetCursorPosition(x, y+3);                              //  //
            Console.WriteLine("╔════════════════╦════════════════╗");       //  //
            for (int i = 0; i < 4; i++)                                     //  //
            {                                                               //  //  Draw Stats
                Console.WriteLine("║                ║                ║");   //  //
            }                                                               //  //
            Console.WriteLine("╚════════════════╩════════════════╝");       //  //


            if (message != null)                            //
            {                                               //
                Console.SetCursorPosition(x + 1, y + 1);    //  Write Message/Interaction
                Console.Write(message);                     //
            }                                               //


            Console.SetCursorPosition(x + 1, y + 4);                            //
            Console.Write("Player");                                            //
            Console.SetCursorPosition(x + 1, y + 5);                            //
            Console.Write("HP: " + player.GetHealth() + "/" + player.GetMaxHealth());             //  Write Player Stats
            Console.SetCursorPosition(x + 1, y + 6);                            //
            Console.Write("Shield: " + player.GetShield() + "/" + player.GetMaxShield()); //
            Console.SetCursorPosition(x + 1, y + 7);                            //
            Console.Write("ATK: " + player.GetATK());                                //


            if (enemyManager.GetLastAttacked() != null)                      //
            {                                                           //
                enemy = enemyManager.GetLastAttacked();                      //
                Console.SetCursorPosition(x + 18, y + 4);               //
                Console.Write(enemy.GetName());                              //  Write Stats of Last Attacked Enemy
                Console.SetCursorPosition(x + 18, y + 5);               //
                Console.Write("HP: " + enemy.GetHealth() + "/" + enemy.GetMaxHealth());   //
                Console.SetCursorPosition(x + 18, y + 6);               //
                Console.Write("ATK: " + enemy.GetATK());                     //
            }                                                           //
        */
        }

        public void SetMessage(string message)
        {
            this.message = message;
        }


    }
}
