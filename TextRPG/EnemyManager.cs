using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class EnemyManager
    {
        public List<Enemy> Enemies = new List<Enemy>();
        public Map map;
        public Player player;
        public Random random = new Random();
        public bool toMove;
        public Render rend;
        public ItemManager itemManager;
        public Enemy lastAttacked;
        public GameManager gManager;

        public EnemyManager(Map map, Render rend)
        {
            this.map = map;
            this.rend = rend;
        }

        public void GenerateEnemies()
        {
            for (int i = 3; i < 35; i += 7)
            {
                for (int j = 3; j < 35; j += 7)
                {
                    if(player.PlayerCheck(i,j) == false && itemManager.ItemChecks(i,j) == null)
                    {
                        int chance = random.Next(10);
                        switch (chance)
                        {
                            case 0:
                            case 1:
                                Enemies.Add(new Enemy(i, j, new EnemyType(EnemyType.Type.slime), map, player, this, rend, gManager));
                                break;
                            case 2:
                                Enemies.Add(new Enemy(i, j, new EnemyType(EnemyType.Type.goblin), map, player, this, rend, gManager));
                                break;
                            case 3:
                            case 4:
                                Enemies.Add(new Enemy(i, j, new EnemyType(EnemyType.Type.kobold), map, player, this, rend, gManager));
                                break;
                            case 5:
                            case 6:
                            case 7:
                            case 8:
                            case 9:
                                break;
                        }
                    }
                }
            }
        }

        public void UpdateEnemies()
        {
            if (toMove)
            {
                foreach(Enemy enemy in Enemies)
                {
                    enemy.Move(random);
                }
            }
            toMove = !toMove;
        }

        public Enemy EnemyCheck(int x, int y, bool isAttack)
        {
            Enemy foundEnemy = null;
            foreach(Enemy enemy in Enemies)
            {
                if(enemy.x == x && enemy.y == y)
                {
                    foundEnemy = enemy;
                }
            }
            if (isAttack) lastAttacked = foundEnemy;
            return foundEnemy;
        }

        public void DrawEnemies()
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw();
            }
        }

        public void RemoveEnemy(Enemy enemy)
        {
            if (Enemies.Contains(enemy))
            {
                Enemies.Remove(enemy);
            }
        }

    }
}
