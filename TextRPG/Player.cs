using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Player : GameCharacter
    {
        private ConsoleKey key;
        private int shield;
        private int maxShield;
        private InputManager inputManager;
        private ItemManager itemManager;
        private int targetX;
        private int targetY;

        public Player(int x, int y, Map map, EnemyManager enemyManager, Render rend, GameManager manager, InputManager inputManager, ItemManager itemManager) : base(x, y, Constants.playerBaseHP, Constants.playerBaseAttack,Constants.playerSprite,map,enemyManager, Constants.playerColor, rend, manager)
        {
            shield = Constants.playerBaseShield;
            maxShield = Constants.playerBaseShield;
            this.inputManager = inputManager;
            this.itemManager = itemManager;
        }

        public void Update()
        {
            manager.setMessage("");    //  Clears Interaction Message

            key = inputManager.GetKey();                                                                //  Moving
            targetX = x;
            targetY = y;
            switch (key)                                                                                //
            {                                                                                           //
                case ConsoleKey.W:                                                                      //  //  Moving Up
                case ConsoleKey.UpArrow:                                                                //  //
                    targetY--;
                    break;                                                                              //  //
                case ConsoleKey.S:                                                                      //      //  Moving Down
                case ConsoleKey.DownArrow:                                                              //      //
                    targetY++;
                    break;                                                                              //      //
                case ConsoleKey.A:                                                                      //  //  Moving Left
                case ConsoleKey.LeftArrow:                                                              //  //
                    targetX--;
                    break;                                                                              //  //
                case ConsoleKey.D:                                                                      //      //  Moving Right
                case ConsoleKey.RightArrow:                                                             //      //
                    targetX++;
                    break;                                                                              //      //
            }                                                                                           //
            if(map.isFloorAt(targetX, targetY) && enemyManager.EnemyAt(targetX, targetY, false) == null && itemManager.ItemAt(targetX, targetY) == null)
            {
                x = targetX;
                y = targetY;
            }else if (enemyManager.EnemyAt(targetX, targetY, false) != null)
            {
                AttackEnemy(enemyManager.EnemyAt(targetX, targetY, true));
            }else if (itemManager.ItemAt(targetX, targetY) != null)
            {
                itemManager.PickUp(itemManager.ItemAt(targetX, targetY), this);
            }
        }

        public bool isPlayerAt(int x, int y)   //returns true if the provided coordinates are the player's coordinates
        {
            bool check = false;
            if (this.x == x && this.y == y) check = true;
            return check;
        }

        public void AttackEnemy(Enemy enemy) //Attacks the provided enemy and gives the interaction message
        {
            enemy.TakeDMG(ATK);
            if (enemy.GetHealth() > 0) manager.setMessage("Player attacked " + enemy.GetName());
            else manager.setMessage("Player killed " + enemy.GetName());
        }

        public override void TakeDMG(int DMG)
        {
            if(shield > DMG)        //
            {                       //
                shield -= DMG;      //  Damages shield if dmg wouldn't destroy it
                base.TakeDMG(0);    //
            }                       //
            else                        //
            {                           //
                DMG -= shield;          //  Shield reduces dmg (if it isn't 0 already), before being destroyed and applying dmg normally
                shield = 0;             //
                base.TakeDMG(DMG);      //
            }                           //
        }

        public void Heal(int heal)  //  Restores HP up to max
        {
            HP += heal;
            if(HP > maxHP)
            {
                HP = maxHP;
            }
        }

        public void RestoreShield(int restore)  //  Restores Shield up to max
        {
            shield += restore;
            if(shield > maxShield)
            {
                shield = maxShield;
            }
        }

        public void RaiseATK(int raise) //  Increase ATK power
        {
            ATK += raise;
        }

        public int GetShield()
        {
            return shield;
        }

        public int GetMaxShield()
        {
            return maxShield;
        }

        

    }
}
