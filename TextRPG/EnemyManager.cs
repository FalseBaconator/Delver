using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class EnemyManager
    {
        private List<Enemy> Enemies = new List<Enemy>();
        private Map map;
        private Random random = new Random();
        private bool toMove;
        private Render rend;
        private ItemManager itemManager;
        private Enemy lastAttacked;
        GameManager manager;

        public EnemyManager(Map map, Render rend, ItemManager itemManager, GameManager manager)
        {
            this.map = map;
            this.rend = rend;
            this.itemManager = itemManager;
            this.manager = manager;
        }

        public void GenerateEnemies(Player player)
        {
            for (int i = 3; i < 35; i += 7)     //
            {                                   //  Center of each room
                for (int j = 3; j < 35; j += 7) //
                {
                    if(player.isPlayerAt(i,j) == false && itemManager.ItemAt(i,j) == null && map.isFloorAt(i,j))                    //
                    {                                                                                                                   //
                        int chance = random.Next(10);                                                                                   //
                        switch (chance)                                                                                                 //
                        {                                                                                                               //
                            case 0:                                                                                                     //
                            case 1:                                                                                                     //
                                Enemies.Add(new Enemy(i, j, new EnemyType(EnemyType.Type.slime), map, player, this, rend, manager));    //
                                break;                                                                                                  //
                            case 2:                                                                                                     //  Chance of generating enemy on valid tiles
                                Enemies.Add(new Enemy(i, j, new EnemyType(EnemyType.Type.goblin), map, player, this, rend, manager));   //
                                break;                                                                                                  //
                            case 3:                                                                                                     //
                            case 4:                                                                                                     //
                                Enemies.Add(new Enemy(i, j, new EnemyType(EnemyType.Type.kobold), map, player, this, rend, manager));   //
                                break;                                                                                                  //
                            case 5:                                                                                                     //
                            case 6:                                                                                                     //
                            case 7:                                                                                                     //
                            case 8:                                                                                                     //
                            case 9:                                                                                                     //
                                break;                                                                                                  //
                        }                                                                                                               //
                    }
                }
            }
        }

        public void UpdateEnemies() //Move each enemy on every other turn
        {
            if (toMove)
            {
                foreach(Enemy enemy in Enemies)
                {
                    enemy.Update(random);
                }
            }
            toMove = !toMove;
        }

        public Enemy EnemyAt(int x, int y, bool isAttack)    //Returns the enemy at the provided coords. Saves lastAttacked enemy if attacking
        {
            Enemy foundEnemy = null;
            foreach(Enemy enemy in Enemies)
            {
                if(enemy.GetX() == x && enemy.GetY() == y)
                {
                    foundEnemy = enemy;
                }
            }
            if (isAttack) lastAttacked = foundEnemy;
            return foundEnemy;
        }

        public void DrawEnemies()   //Save enemies to rend arrays
        {
            foreach (Enemy enemy in Enemies)
            {
                enemy.Draw();
            }
        }

        public void RemoveEnemy(Enemy enemy)    //Removes enemy from enemies array
        {
            if (Enemies.Contains(enemy))
            {
                Enemies.Remove(enemy);
            }
        }

        public int GetEnemyCount()
        {
            return Enemies.Count();
        }

        public Enemy GetLastAttacked()
        {
            return lastAttacked;
        }

    }
}
