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

        EnemyType type;

        //Random random = new Random();

        private bool moved;

        /*public Enemy(int x, int y, int HP, int ATK, char sprite, Map map, ConsoleColor color, Player player, EnemyManager enemyManager, Render rend) : base(x, y, HP, ATK, sprite, map, enemyManager, color, rend)
        {
            this.player = player;
        }
        */

        public Enemy(int x, int y, EnemyType type, Map map, Player player, EnemyManager enemyManager, Render rend) : base(x,y,type.HP, type.ATK, type.sprite, map, enemyManager, type.color, rend)
        {
            this.type = type;
            this.player = player;
        }

        public void Move(Random random)
        {
            if (alive)
            {
                moved = false;
                
                if(player.PlayerCheck(x,y-1) || player.PlayerCheck(x, y + 1) || player.PlayerCheck(x-1, y) || player.PlayerCheck(x+1, y))
                {
                    Attack(player);
                    moved = true;
                }

                while (moved == false)
                {
                    int Dir = random.Next(4);
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
