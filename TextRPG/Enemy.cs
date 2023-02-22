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

        public string name;

        private int sightRange = 5;

        private GameManager gManager;

        /*public Enemy(int x, int y, int HP, int ATK, char sprite, Map map, ConsoleColor color, Player player, EnemyManager enemyManager, Render rend) : base(x, y, HP, ATK, sprite, map, enemyManager, color, rend)
        {
            this.player = player;
        }
        */

        public Enemy(int x, int y, EnemyType type, Map map, Player player, EnemyManager enemyManager, Render rend, GameManager gManager) : base(x, y, type.HP, type.ATK, type.sprite, map, enemyManager, type.color, rend)
        {
            this.type = type;
            this.player = player;
            name = this.type.name;
            this.gManager = gManager;
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
                    if (type.type == EnemyType.Type.slime || CanSeePlayer() == false)
                    {
                        switch (Dir)
                        {
                            case 0:
                                if (map.CheckTile(x, y - 1) && player.PlayerCheck(x, y - 1) == false && enemyManager.EnemyCheck(x, y - 1, false) == null)
                                {
                                    y--;
                                    moved = true;
                                }
                                break;
                            case 1:
                                if (map.CheckTile(x, y + 1) && player.PlayerCheck(x, y + 1) == false && enemyManager.EnemyCheck(x, y + 1, false) == null)
                                {
                                    y++;
                                    moved = true;
                                }
                                break;
                            case 2:
                                if (map.CheckTile(x - 1, y) && player.PlayerCheck(x - 1, y) == false && enemyManager.EnemyCheck(x - 1, y, false) == null)
                                {
                                    x--;
                                    moved = true;
                                }
                                break;
                            case 3:
                                if (map.CheckTile(x + 1, y) && player.PlayerCheck(x + 1, y) == false && enemyManager.EnemyCheck(x + 1, y, false) == null)
                                {
                                    x++;
                                    moved = true;
                                }
                                break;
                        }
                    }
                    else
                    {
                        int deltaX = player.x - x;
                        int deltaY = player.y - y;
                        switch (type.type)
                        {
                            case EnemyType.Type.goblin: //chase
                                if(deltaX >= 0 && deltaY >= 0)
                                {
                                    if(deltaX >= deltaY && map.CheckTile(x + 1, y) && player.PlayerCheck(x + 1, y) == false && enemyManager.EnemyCheck(x + 1, y, false) == null)
                                    {
                                        x++;
                                        
                                    }
                                    else if(map.CheckTile(x, y + 1) && player.PlayerCheck(x, y + 1) == false && enemyManager.EnemyCheck(x, y + 1, false) == null)
                                    {
                                        
                                        y++;
                                        
                                    }
                                }else if (deltaX >= 0 && deltaY < 0)
                                {
                                    if (deltaX >= deltaY * -1 && map.CheckTile(x + 1, y) && player.PlayerCheck(x + 1, y) == false && enemyManager.EnemyCheck(x + 1, y, false) == null)
                                    {
                                        x++;
                                    }
                                    else if (map.CheckTile(x, y - 1) && player.PlayerCheck(x, y - 1) == false && enemyManager.EnemyCheck(x, y - 1, false) == null)
                                    {
                                        y--;
                                        
                                    }
                                }else if (deltaX < 0 && deltaY >= 0)
                                {
                                    if (deltaX * -1 >= deltaY && map.CheckTile(x - 1, y) && player.PlayerCheck(x - 1, y) == false && enemyManager.EnemyCheck(x - 1, y, false) == null)
                                    {
                                        x--;
                                    }
                                    else if (map.CheckTile(x, y + 1) && player.PlayerCheck(x, y + 1) == false && enemyManager.EnemyCheck(x, y + 1, false) == null)
                                    {
                                        y++;
                                    }
                                }
                                if (deltaX < 0 && deltaY < 0)
                                {
                                    if (deltaX * -1 >= deltaY * -1 && map.CheckTile(x - 1, y) && player.PlayerCheck(x - 1, y) == false && enemyManager.EnemyCheck(x - 1, y, false) == null)
                                    {
                                        x--;
                                    }
                                    else if (map.CheckTile(x, y - 1) && player.PlayerCheck(x, y - 1) == false && enemyManager.EnemyCheck(x, y - 1, false) == null)
                                    {
                                        y--;
                                    }
                                }
                                break;
                            case EnemyType.Type.kobold: //flee
                                if (deltaX < 0 && deltaY < 0)
                                {
                                    if (deltaX * -1 >= deltaY * -1 && map.CheckTile(x + 1, y) && player.PlayerCheck(x + 1, y) == false && enemyManager.EnemyCheck(x + 1, y, false) == null)
                                    {
                                        x++;
                                    }
                                    else if (map.CheckTile(x, y + 1) && player.PlayerCheck(x, y + 1) == false && enemyManager.EnemyCheck(x, y + 1, false) == null)
                                    {
                                        y++;
                                    }
                                }
                                else if (deltaX < 0 && deltaY >= 0)
                                {
                                    if (deltaX * -1 >= deltaY && map.CheckTile(x + 1, y) && player.PlayerCheck(x + 1, y) == false && enemyManager.EnemyCheck(x + 1, y, false) == null)
                                    {
                                        x++;
                                    }
                                    else if (map.CheckTile(x, y - 1) && player.PlayerCheck(x, y - 1) == false && enemyManager.EnemyCheck(x, y - 1, false) == null)
                                    {
                                        y--;
                                    }
                                }
                                else if (deltaX >= 0 && deltaY < 0)
                                {
                                    if (deltaX >= deltaY * -1 && map.CheckTile(x - 1, y) && player.PlayerCheck(x - 1, y) == false && enemyManager.EnemyCheck(x - 1, y, false) == null)
                                    {
                                        x--;
                                    }
                                    else if (map.CheckTile(x, y + 1) && player.PlayerCheck(x, y + 1) == false && enemyManager.EnemyCheck(x, y + 1, false) == null)
                                    {
                                        y++;
                                    }
                                }
                                if (deltaX >= 0 && deltaY >= 0)
                                {
                                    if (deltaX >= deltaY && map.CheckTile(x - 1, y) && player.PlayerCheck(x - 1, y) == false && enemyManager.EnemyCheck(x - 1, y, false) == null)
                                    {
                                        x--;
                                    }
                                    else if (map.CheckTile(x, y - 1) && player.PlayerCheck(x, y - 1) == false && enemyManager.EnemyCheck(x, y - 1, false) == null)
                                    {
                                        y--;
                                    }
                                }
                                break;
                        }
                        moved = true;
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
            if (target.HP <= 0) gManager.setMessage(name + " killed Player");
            else if (gManager.message == "") gManager.setMessage(name + " attacked Player");
            else gManager.setMessage("Player and " + name + " both attacked");
        }

        public bool CanSeePlayer()
        {
            bool check = false;

            int a2 = (player.x - x);
            a2 = a2 * a2;

            int b2 = (player.y - y);
            b2 = b2 * b2;

            int c = (int)Math.Sqrt(a2+b2);

            if (c <= sightRange)
            {
                check = true;
            }

            return check;
        }



    }
}
