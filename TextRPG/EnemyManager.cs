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
        Exit exit;

        public EnemyManager(Map map, Render rend, ItemManager itemManager, GameManager manager, Exit exit)
        {
            this.map = map;
            this.rend = rend;
            this.itemManager = itemManager;
            this.manager = manager;
            this.exit = exit;
        }

        public void GenerateEnemies(Player player)
        {
            int tempX;
            int tempY;
            Enemies.Clear();
            int placedEnemies = 0;
            while(placedEnemies < Constants.EnemyAmount)
            {
                //Console.WriteLine("Attempt(Enemy)");
                tempX = random.Next(Constants.mapWidth * Constants.roomWidth);
                tempY = random.Next(Constants.mapHeight * Constants.roomHeight);
                if((Math.Abs(player.GetX() - tempX) > 5 || Math.Abs(player.GetY() - tempY) > 5) && map.isFloorAt(tempX, tempY) && itemManager.ItemAt(tempX, tempY) == null && exit.isExitAt(tempX, tempY, false) == false && EnemyAt(tempX, tempY, false) == null)
                {
                    switch (random.Next(5))
                    {
                        case 0:
                        case 1:
                            Enemies.Add(new Slime(tempX, tempY, map, player, this, itemManager, rend, manager));
                            placedEnemies++;
                            break;
                        case 2:
                        case 3:
                            Enemies.Add(new Kobold(tempX, tempY, map, player, this, itemManager, rend, manager));
                            placedEnemies++;
                            break;
                        case 4:
                            Enemies.Add(new Goblin(tempX, tempY, map, player, this, itemManager, rend, manager));
                            placedEnemies++;
                            break;
                    }
                }
            }
        }

        public void GenerateBoss(Player player)
        {
            int tempX;
            int tempY;
            Enemies.Clear();
            bool placedBoss = false;
            while (placedBoss == false)
            {
                //Console.WriteLine("Attempt(Enemy)");
                tempX = random.Next(Constants.BossRoomWidth);
                tempY = random.Next(Constants.BossRoomHeight);
                if ((Math.Abs(player.GetX() - tempX) > 2 || Math.Abs(player.GetY() - tempY) > 2) && map.isFloorAt(tempX, tempY) && itemManager.ItemAt(tempX, tempY) == null && exit.isExitAt(tempX, tempY, false) == false && EnemyAt(tempX, tempY, false) == null)
                {
                    Enemies.Add(new Boss(tempX, tempY, map, player, this, itemManager, rend, manager));
                    placedBoss = true;
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
