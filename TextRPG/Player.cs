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
        private Exit exit;

        public Player(Position pos, Map map, EnemyManager enemyManager, Render rend, GameManager manager, InputManager inputManager, ItemManager itemManager, Exit exit) : base(pos, Constants.playerBaseHP, Constants.playerBaseAttack, Constants.playerSprite, map, enemyManager, rend, manager)
        {
            shield = Constants.playerBaseShield;
            maxShield = Constants.playerBaseShield;
            this.inputManager = inputManager;
            this.itemManager = itemManager;
            this.exit = exit;
        }

        public void Update()
        {
            key = inputManager.GetKey();                                                                //
            targetPos = pos;
            switch (key)                                                                                //
            {                                                                                           //
                case ConsoleKey.W:                                                                      //
                case ConsoleKey.UpArrow:                                                                //
                    targetPos.y--;                                                                          //
                    break;                                                                              //
                case ConsoleKey.S:                                                                      //
                case ConsoleKey.DownArrow:                                                              //  Pick Direction
                    targetPos.y++;                                                                          //
                    break;                                                                              //
                case ConsoleKey.A:                                                                      //
                case ConsoleKey.LeftArrow:                                                              //
                    targetPos.x--;                                                                          //
                    break;                                                                              //
                case ConsoleKey.D:                                                                      //
                case ConsoleKey.RightArrow:                                                             //
                    targetPos.x++;                                                                          //
                    break;                                                                              //
            }                                                                                           //
            if(map.isFloorAt(targetPos) && enemyManager.EnemyAt(targetPos, false) == null && itemManager.ItemAt(targetPos) == null)    //
            {                                                                                                                                               //  Move if empty floor
                pos = targetPos;                                                                                                                            //
            }else if (enemyManager.EnemyAt(targetPos, false) != null)    //
            {                                                                   //  Attack enemy in target space
                AttackEnemy(enemyManager.EnemyAt(targetPos, true));      //
            }else if (itemManager.ItemAt(targetPos) != null)                 //
            {                                                                       //  Pick Up item in target space
                itemManager.PickUp(itemManager.ItemAt(targetPos), this);     //
            }                                                                       //

            exit.isExitAt(targetPos, true);
        }

        public bool isPlayerAt(Position pos)   //returns true if the provided coordinates are the player's coordinates
        {
            bool check = false;
            if (this.pos == pos) check = true;
            return check;
        }

        public void placePlayer(Position pos)
        {
            this.pos = pos;
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
