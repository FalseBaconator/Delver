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

        public Hud(Player player, EnemyManager enemyManager, int x, int y)
        {
            this.player = player;
            this.enemyManager = enemyManager;
            this.x = x;
            this.y = y;
        }

        public void draw()
        {
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
        }

        public void SetMessage(string message)
        {
            this.message = message;
        }

    }
}
