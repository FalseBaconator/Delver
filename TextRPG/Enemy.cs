using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Enemy : GameCharacter
    {
        Player player;
        
        private bool moved;

        public Enemy(int x, int y, int HP, int ATK, char sprite, Map map, ConsoleColor color, Player player, EnemyManager enemyManager) : base(x, y, HP, ATK, sprite, map, enemyManager, color)
        {
            this.player = player;
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
                if(player.PlayerCheck(x,y-1) || player.PlayerCheck(x, y + 1) || player.PlayerCheck(x-1, y) || player.PlayerCheck(x+1, y))
                {
                    Attack(player);
                    moved = true;
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
                            }
                            break;
                        case 1:
                            if (map.CheckTile(x, y + 1) && player.PlayerCheck(x, y + 1) == false && enemyManager.EnemyCheck(x, y + 1) == null)
                            {
                                y++;
                                moved = true;
                            }
                            break;
                        case 2:
                            if (map.CheckTile(x - 1, y) && player.PlayerCheck(x - 1, y) == false && enemyManager.EnemyCheck(x - 1, y) == null)
                            {
                                x--;
                                moved = true;
                            }
                            break;
                        case 3:
                            if (map.CheckTile(x + 1, y) && player.PlayerCheck(x + 1, y) == false && enemyManager.EnemyCheck(x + 1, y) == null)
                            {
                                x++;
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


        public override void TakeDMG(int DMG)
        {
            base.TakeDMG(DMG);
            if (HP <= 0) enemyManager.RemoveEnemy(this);
        }

        public void Attack(Player target)
        {
            target.TakeDMG(ATK);
        }

    }
}
