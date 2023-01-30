using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class EnemyManager
    {
        public List<Enemy> Enemies;
        public Map map;
        public Player player;
        public bool toMove;

        /*public EnemyManager(Enemy[] Enemies)
        {
            this.Enemies = Enemies;
        }*/

        public void MoveEnemies()
        {
            if (toMove)
            {
                foreach(Enemy enemy in Enemies)
                {
                    enemy.Move();
                }
            }
            toMove = !toMove;
        }

        public Enemy EnemyCheck(int x, int y)
        {
            Enemy foundEnemy = null;
            foreach(Enemy enemy in Enemies)
            {
                if(enemy.x == x && enemy.y == y)
                {
                    foundEnemy = enemy;
                }
            }
            return foundEnemy;
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
