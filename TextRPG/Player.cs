using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Player : GameCharacter
    {
        public InputManager inputManager;
        private ConsoleKey key;
        public int shield;
        public int maxShield;
        public ItemManager itemManager;
        public GameManager gManager;

        public Player(int x, int y, int HP, int shield, int ATK, char sprite, Map map, EnemyManager enemyManager, ConsoleColor color, Render rend) : base(x,y,HP,ATK,sprite,map,enemyManager,color, rend)
        {
            enemyManager.player = this;
            maxHP = HP;
            this.shield = shield;
            maxShield = shield;
        }

        public void Update()
        {
            gManager.setMessage("");

            key = inputManager.GetKey();
            switch (key)
            {
                case ConsoleKey.W:
                case ConsoleKey.UpArrow:
                    if(map.CheckTile(x, y - 1) && enemyManager.EnemyCheck(x,y-1, false) == null)
                    {
                        y--;
                    }else if(enemyManager.EnemyCheck(x,y-1, false) != null)
                    {
                        Attack(enemyManager.EnemyCheck(x, y - 1, true));
                    }
                    break;
                case ConsoleKey.S:
                case ConsoleKey.DownArrow:
                    if (map.CheckTile(x, y + 1) && enemyManager.EnemyCheck(x, y + 1, false) == null)
                    {
                        y++;
                    }
                    else if (enemyManager.EnemyCheck(x, y + 1, false) != null)
                    {
                        Attack(enemyManager.EnemyCheck(x, y + 1, true));
                    }
                    break;
                case ConsoleKey.A:
                case ConsoleKey.LeftArrow:
                    if (map.CheckTile(x-1, y) && enemyManager.EnemyCheck(x - 1, y, false) == null)
                    {
                        x--;
                    }
                    else if (enemyManager.EnemyCheck(x - 1, y, false) != null)
                    {
                        Attack(enemyManager.EnemyCheck(x - 1, y, true));
                    }
                    break;
                case ConsoleKey.D:
                case ConsoleKey.RightArrow:
                    if (map.CheckTile(x + 1, y) && enemyManager.EnemyCheck(x + 1, y, false) == null)
                    {
                        x++;
                    }
                    else if (enemyManager.EnemyCheck(x + 1, y, false) != null)
                    {
                        Attack(enemyManager.EnemyCheck(x + 1, y, true));
                    }
                    break;
            }
            if(itemManager.ItemChecks(x,y) != null)
            {
                itemManager.PickUp(itemManager.ItemChecks(x,y));
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
            if (enemy.HP > 0) gManager.setMessage("Player attacked " + enemy.name);
            else gManager.setMessage("Player killed " + enemy.name);
        }

        public override void TakeDMG(int DMG)
        {
            if(shield > DMG)
            {
                shield -= DMG;
                base.TakeDMG(0);
            }
            else
            {
                DMG -= shield;
                shield = 0;
                base.TakeDMG(DMG);
            }
        }

        public void Heal(int heal)
        {
            HP += heal;
            if(HP > maxHP)
            {
                HP = maxHP;
            }
        }

        public void RestoreShield(int restore)
        {
            shield += restore;
            if(shield > maxShield)
            {
                shield = maxShield;
            }
        }

        public void RaiseATK(int raise)
        {
            ATK += raise;
        }

    }
}
