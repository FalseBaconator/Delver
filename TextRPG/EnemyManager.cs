using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class EnemyManager
    {
        private List<Enemy> enemies = new List<Enemy>();
        private Enemy[,] enemyMap = new Enemy[GameManager.constants.mapHeight * GameManager.constants.roomHeight, GameManager.constants.mapWidth * GameManager.constants.roomWidth];
        private Map map;
        private Random random = GameManager.constants.rand;
        private bool toMove;
        private Render rend;
        private ItemManager itemManager;
        private Enemy lastAttacked;
        GameManager manager;
        Exit exit;
        private Hud hud;
        private SoundManager soundManager;
        public event EventHandler<KilledEnemyEventArgs> EnemyKilled;
        private int enemiesKilled;

        private string[] dataArray;
        private string[] bossDataArray;
        private int enemyTypes;
        private int bossTypes;

        public void SetHud(Hud hud)
        {
            this.hud = hud;
        }

        public EnemyManager(Map map, Render rend, ItemManager itemManager, GameManager manager, Exit exit, SoundManager soundManager)
        {
            this.map = map;
            this.rend = rend;
            this.itemManager = itemManager;
            this.manager = manager;
            this.exit = exit;
            this.soundManager = soundManager;

            dataArray = File.ReadAllLines("Data/EnemyData.txt");
            bossDataArray = File.ReadAllLines("Data/BossData.txt");

            enemyTypes = (int)dataArray.Count() / 7;
            bossTypes = (int)bossDataArray.Count() / 7;

        }

        public void GenerateEnemies(Player player)
        {
            enemiesKilled = 0;
            Position tempPos;
            ClearEnemies();
            int placedEnemies = 0;
            while (placedEnemies < GameManager.constants.EnemyAmount)
            {
                tempPos = new Position(random.Next(GameManager.constants.mapWidth * GameManager.constants.roomWidth), random.Next(GameManager.constants.mapHeight * GameManager.constants.roomHeight));
                if ((Math.Abs(player.GetPos().x - tempPos.x) > 5 || Math.Abs(player.GetPos().y - tempPos.y) > 5) && map.isFloorAt(tempPos) && itemManager.ItemAt(tempPos) == null && exit.isExitAt(tempPos, false) == false && EnemyAt(tempPos, false) == null)
                {
                    int toPlace = random.Next(enemyTypes);
                    int offset = toPlace * 7;
                    enemies.Add(new Enemy(tempPos, Constants.GetInt(dataArray[(offset + 2)]), Constants.GetInt(dataArray[offset + 1]), Constants.GetTile(dataArray[offset + 6]), Constants.GetString(dataArray[offset]), Enemy.GetBehavior(dataArray[offset + 5]), false, map, player, this, itemManager, rend, manager, hud, exit, Constants.GetInt(dataArray[offset + 3]), Constants.GetInt(dataArray[offset + 4]), soundManager));
                    enemyMap[tempPos.x, tempPos.y] = enemies[placedEnemies];
                    placedEnemies++;
                }
            }


        }

        public void GenerateBoss(Player player)
        {
            Position tempPos;
            ClearEnemies();
            bool placedBoss = false;
            while (placedBoss == false)
            {
                //Console.WriteLine("Attempt(Enemy)");\
                tempPos = new Position(random.Next(GameManager.constants.BossRoomWidth), random.Next(GameManager.constants.BossRoomHeight));
                if ((Math.Abs(player.GetPos().x - tempPos.x) > 2 || Math.Abs(player.GetPos().y - tempPos.y) > 2) && map.isFloorAt(tempPos) && itemManager.ItemAt(tempPos) == null && exit.isExitAt(tempPos, false) == false && EnemyAt(tempPos, false) == null)
                {
                    //enemies.Add(new Boss(tempPos, map, player, this, itemManager, rend, manager, hud, exit, soundManager));
                    int toPlace = random.Next(bossTypes);
                    int offset = toPlace * 7;
                    enemies.Add(new Enemy(tempPos, Constants.GetInt(bossDataArray[(offset + 2)]), Constants.GetInt(bossDataArray[offset + 1]), Constants.GetTile(bossDataArray[offset + 6]), Constants.GetString(bossDataArray[offset]), Enemy.GetBehavior(bossDataArray[offset + 5]), true, map, player, this, itemManager, rend, manager, hud, exit, Constants.GetInt(bossDataArray[offset + 3]), Constants.GetInt(bossDataArray[offset + 4]), soundManager));
                    placedBoss = true;
                    enemyMap[tempPos.x, tempPos.y] = enemies[0];
                }
            }
        }

        public void UpdateEnemies() //Move each enemy on every other turn
        {
            if (toMove)
            {
                foreach (Enemy enemy in enemies)
                {
                    enemyMap[enemy.GetPos().x, enemy.GetPos().y] = null;
                    enemy.Update();
                    enemyMap[enemy.GetPos().x, enemy.GetPos().y] = enemy;
                }
            }
            toMove = !toMove;
        }

        public Enemy EnemyAt(Position pos, bool isAttack)    //Returns the enemy at the provided coords. Saves lastAttacked enemy if attacking
        {
            Enemy foundEnemy = enemyMap[pos.x, pos.y];
            if (isAttack && foundEnemy != null) lastAttacked = foundEnemy;
            return foundEnemy;
        }

        public void DrawEnemies()   //Save enemies to rend arrays
        {
            foreach (Enemy enemy in enemies)
            {
                enemy.Draw();
            }
        }

        public void RemoveEnemy(Enemy enemy)    //Removes enemy from enemies array
        {
            if (enemies.Contains(enemy))
            {
                enemies.Remove(enemy);
                enemyMap[enemy.GetPos().x, enemy.GetPos().y] = null;
                OnEnemyKilled();
            }
        }

        public int GetEnemyCount()
        {
            return enemies.Count();
        }

        public Enemy GetLastAttacked()
        {
            return lastAttacked;
        }

        public void ClearEnemies()
        {
            foreach (Enemy enemy in enemies)
            {
                enemyMap[enemy.GetPos().x, enemy.GetPos().y] = null;
            }
            enemies.Clear();
        }

        protected virtual void OnEnemyKilled()
        {
            enemiesKilled++;
            if (EnemyKilled != null)
                EnemyKilled(this, new KilledEnemyEventArgs() { enemiesKilled = this.enemiesKilled});
        }
    }

    internal class KilledEnemyEventArgs : EventArgs
    {
        public int enemiesKilled;
    }
}