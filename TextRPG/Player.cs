using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Player : GameCharacter
    {
        

        public Player(int x, int y, int HP, int ATK, char sprite, Map map, EnemyManager enemyManager, ConsoleColor color) : base(x,y,HP,ATK,sprite,map,enemyManager,color)
        {
            enemyManager.player = this;
            DisplayHP();
        }

        public void Move(ConsoleKey key)
        {
            if (x < Console.WindowWidth && x > 0 && y < Console.WindowHeight && y > 0)
            {
                map.DrawTile(x, y);
            }

            switch (key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    if(map.CheckTile(x, y - 1) && enemyManager.EnemyCheck(x,y-1) == null)
                    {
                        y--;
                    }else if(enemyManager.EnemyCheck(x,y-1) != null)
                    {
                        Attack(enemyManager.EnemyCheck(x, y - 1));
                    }
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    if (map.CheckTile(x, y + 1) && enemyManager.EnemyCheck(x, y + 1) == null)
                    {
                        y++;
                    }
                    else if (enemyManager.EnemyCheck(x, y + 1) != null)
                    {
                        Attack(enemyManager.EnemyCheck(x, y + 1));
                    }
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    if (map.CheckTile(x-1, y) && enemyManager.EnemyCheck(x - 1, y) == null)
                    {
                        x--;
                    }
                    else if (enemyManager.EnemyCheck(x - 1, y) != null)
                    {
                        Attack(enemyManager.EnemyCheck(x - 1, y));
                    }
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    if (map.CheckTile(x + 1, y) && enemyManager.EnemyCheck(x + 1, y) == null)
                    {
                        x++;
                    }
                    else if (enemyManager.EnemyCheck(x + 1, y) != null)
                    {
                        Attack(enemyManager.EnemyCheck(x + 1, y));
                    }
                    break;
            }

            

            if (x < Console.WindowWidth && x > 0 && y < Console.WindowHeight && y > 0)
            {
                Console.SetCursorPosition(x, y);
                Console.ForegroundColor = color;
                Console.Write(sprite);
            }
        }

        public bool PlayerCheck(int x, int y)
        {
            bool check = false;
            if (this.x == x && this.y == y) check = true;
            return check;
        }

        public void Attack(Enemy enemy)
        {
            enemy.TakeDMG(ATK);
        }

        public override void TakeDMG(int DMG)
        {
            base.TakeDMG(DMG);
            DisplayHP();
        }

        public void DisplayHP()
        {
            Console.ResetColor();
            Console.SetCursorPosition(45, 10);
            Console.Write("HP: " + HP);
        }

    }
}
