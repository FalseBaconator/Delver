using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    abstract class Enemy : GameCharacter
    {
        protected Player player;

        protected bool moved;

        protected string name;

        protected int sightRange = Constants.EnemySightRange;

        protected ItemManager itemManager;

        public Enemy(int x, int y, int HP, int ATK, char sprite, string name, Map map, Player player, EnemyManager enemyManager, ConsoleColor color, ItemManager itemManager, Render rend, GameManager manager) : base(x, y, HP, ATK, sprite, map, enemyManager, color, rend, manager)
        {
            this.name = name;
            this.player = player;
            this.itemManager = itemManager;
        }

        public abstract void Update(Random random);

        protected void RandomMove(Random random)
        {
            targetX = x;
            targetY = y;

            int dir = random.Next(4);
            switch (dir)
            {
                case 0:
                    targetY--;
                    break;
                case 1:
                    targetY++;
                    break;
                case 2:
                    targetX--;
                    break;
                case 3:
                    targetX++;
                    break;
            }
            if(map.isFloorAt(targetX, targetY) && enemyManager.EnemyAt(targetX, targetY, false) == null && itemManager.ItemAt(targetX, targetY) == null && player.isPlayerAt(targetX, targetY) == false)
            {
                x = targetX;
                y = targetY;
            }
            
            moved = true;   // moved gets set to true regardless of if a move actually happened to avoid infinite while loops when surrounded

        }

        public override void TakeDMG(int DMG)
        {
            base.TakeDMG(DMG);
            if (HP <= 0) enemyManager.RemoveEnemy(this);
        }

        public void AttackPlayer(Player target)
        {
            target.TakeDMG(ATK);
            if (target.GetHealth() <= 0) manager.setMessage(name + " killed Player");
            else if (manager.GetMessage() == " ") manager.setMessage(name + " attacked Player");
            else manager.setMessage("Player and " + name + " both attacked");
        }

        public bool CanSeePlayer()
        {
            bool check = false;

            int a2 = (player.GetX() - x);
            a2 = a2 * a2;

            int b2 = (player.GetY() - y);
            b2 = b2 * b2;

            int c = (int)Math.Sqrt(a2+b2);

            if (c <= sightRange)
            {
                check = true;
            }

            return check;
        }

        public string GetName()
        {
            return name;
        }

    }
}
