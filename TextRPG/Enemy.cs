using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Enemy
    {
        public int x;
        public int y;
        public int HP;
        public int ATK;
        public char sprite;
        public Map map;
        public ConsoleColor color;
        public Player player;
        public EnemyManager enemyManager;

        private bool moved;
        private bool alive = true;

        public Enemy(int x, int y, int HP, int ATK, char sprite, Map map, ConsoleColor color, Player player, EnemyManager enemyManager)
        {
            this.x = x;
            this.y = y;
            this.HP = HP;
            this.ATK = ATK;
            this.sprite = sprite;
            this.map = map;
            this.color = color;
            this.player = player;
            this.enemyManager = enemyManager;
            Console.SetCursorPosition(this.x, this.y);
            Console.ForegroundColor = color;
            Console.Write(sprite);
        }

        public void Move()
        {
            if (alive)
            {
                moved = false;
                if (x < Console.WindowWidth && x > 0 && y < Console.WindowHeight && y > 0)
                {
                    map.DrawTile(x, y);
                }
                while (moved == false)
                {
                    Random random = new Random();
                    int Dir = random.Next(0, 4);
                    switch (Dir)
                    {
                        case 0:
                            if (map.CheckTile(x, y - 1) && player.PlayerCheck(x, y - 1) == false && enemyManager.EnemyCheck(x, y - 1) == null)
                            {
                                y--;
                                moved = true;
                            }else if (player.PlayerCheck(x, y - 1))
                            {
                                Attack(player);
                                moved = true;
                            }
                            break;
                        case 1:
                            if (map.CheckTile(x, y + 1) && player.PlayerCheck(x, y + 1) == false && enemyManager.EnemyCheck(x, y + 1) == null)
                            {
                                y++;
                                moved = true;
                            }
                            else if (player.PlayerCheck(x, y + 1))
                            {
                                Attack(player);
                                moved = true;
                            }
                            break;
                        case 2:
                            if (map.CheckTile(x - 1, y) && player.PlayerCheck(x - 1, y) == false && enemyManager.EnemyCheck(x - 1, y) == null)
                            {
                                x--;
                                moved = true;
                            }
                            else if (player.PlayerCheck(x, y + 1))
                            {
                                Attack(player);
                                moved = true;
                            }
                            break;
                        case 3:
                            if (map.CheckTile(x + 1, y) && player.PlayerCheck(x + 1, y) == false && enemyManager.EnemyCheck(x + 1, y) == null)
                            {
                                x++;
                                moved = true;
                            }
                            else if (player.PlayerCheck(x, y +  1))
                            {
                                Attack(player);
                                moved = true;
                            }
                            break;
                    }
                }
                if (x < Console.WindowWidth && x > 0 && y < Console.WindowHeight && y > 0)
                {
                    Console.SetCursorPosition(x, y);
                    Console.ForegroundColor = color;
                    Console.Write(sprite);
                }
            }
        }


        public void TakeDamage(int DMG)
        {
            HP -= DMG;
            if(HP <= 0)
            {
                alive = false;
                map.DrawTile(x, y);
                enemyManager.RemoveEnemy(this);
            }
        }

        public void Attack(Player target)
        {
            target.TakeDMG(ATK);
        }

    }
}
