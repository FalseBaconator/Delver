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

        public Player(int x, int y, int HP, int shield, int ATK, char sprite, Map map, EnemyManager enemyManager, ConsoleColor color, Render rend, GameManager manager, InputManager inputManager, ItemManager itemManager) : base(x,y,HP,ATK,sprite,map,enemyManager,color, rend, manager)
        {
            maxHP = HP;
            this.shield = shield;
            maxShield = shield;
            this.inputManager = inputManager;
            this.itemManager = itemManager;
        }

        public void Update()
        {
            manager.setMessage("");    //  Clears Interaction Message

            key = inputManager.GetKey();                                                                //  Moving
            switch (key)                                                                                //
            {                                                                                           //
                case ConsoleKey.W:                                                                      //  //  Moving Up
                case ConsoleKey.UpArrow:                                                                //  //
                    if(map.isFloorAt(x, y - 1) && enemyManager.isEnemyAt(x,y-1, false) == null)         //  //  //
                    {                                                                                   //  //  //  Move Up if empty space
                        y--;                                                                            //  //  //
                    }                                                                                   //  //  //
                    else if(enemyManager.isEnemyAt(x,y-1, false) != null)                               //  //      //
                    {                                                                                   //  //      //  If space is occupied by enemy, Attack
                        AttackEnemy(enemyManager.isEnemyAt(x, y - 1, true));                            //  //      //
                    }                                                                                   //  //      //
                    break;                                                                              //  //
                case ConsoleKey.S:                                                                      //      //  Moving Down
                case ConsoleKey.DownArrow:                                                              //      //
                    if (map.isFloorAt(x, y + 1) && enemyManager.isEnemyAt(x, y + 1, false) == null)     //      //  //
                    {                                                                                   //      //  //  Move if available
                        y++;                                                                            //      //  //
                    }                                                                                   //      //  //
                    else if (enemyManager.isEnemyAt(x, y + 1, false) != null)                           //      //      //
                    {                                                                                   //      //      //  Attack if appropriate
                        AttackEnemy(enemyManager.isEnemyAt(x, y + 1, true));                            //      //      //
                    }                                                                                   //      //      //
                    break;                                                                              //      //
                case ConsoleKey.A:                                                                      //  //  Moving Left
                case ConsoleKey.LeftArrow:                                                              //  //
                    if (map.isFloorAt(x-1, y) && enemyManager.isEnemyAt(x - 1, y, false) == null)       //  //  //
                    {                                                                                   //  //  //  Move if available
                        x--;                                                                            //  //  //
                    }                                                                                   //  //  //
                    else if (enemyManager.isEnemyAt(x - 1, y, false) != null)                           //  //      //
                    {                                                                                   //  //      //  Attack if appropriate
                        AttackEnemy(enemyManager.isEnemyAt(x - 1, y, true));                            //  //      //
                    }                                                                                   //  //      //
                    break;                                                                              //  //
                case ConsoleKey.D:                                                                      //      //  Moving Right
                case ConsoleKey.RightArrow:                                                             //      //
                    if (map.isFloorAt(x + 1, y) && enemyManager.isEnemyAt(x + 1, y, false) == null)     //      //  //
                    {                                                                                   //      //  //  Move if available
                        x++;                                                                            //      //  //
                    }                                                                                   //      //  //
                    else if (enemyManager.isEnemyAt(x + 1, y, false) != null)                           //      //      //
                    {                                                                                   //      //      //  Attack if appropriate
                        AttackEnemy(enemyManager.isEnemyAt(x + 1, y, true));                            //      //      //
                    }                                                                                   //      //      //
                    break;                                                                              //      //
            }                                                                                           //
            if(itemManager.ItemChecks(x,y) != null)                     //
            {                                                           //  Pick up item if on item space
                itemManager.PickUp(itemManager.ItemChecks(x,y), this);  //
            }                                                           //
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
