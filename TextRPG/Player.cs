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
            gManager.setMessage("");    //  Clears Interaction Message

            key = inputManager.GetKey();                                                                //  Moving
            switch (key)                                                                                //
            {                                                                                           //
                case ConsoleKey.W:                                                                      //  //  Moving Up
                case ConsoleKey.UpArrow:                                                                //  //
                    if(map.CheckTile(x, y - 1) && enemyManager.EnemyCheck(x,y-1, false) == null)        //  //  //
                    {                                                                                   //  //  //  Move Up if empty space
                        y--;                                                                            //  //  //
                    }                                                                                   //  //  //
                    else if(enemyManager.EnemyCheck(x,y-1, false) != null)                              //  //      //
                    {                                                                                   //  //      //  If space is occupied by enemy, Attack
                        Attack(enemyManager.EnemyCheck(x, y - 1, true));                                //  //      //
                    }                                                                                   //  //      //
                    break;                                                                              //  //
                case ConsoleKey.S:                                                                      //      //  Moving Down
                case ConsoleKey.DownArrow:                                                              //      //
                    if (map.CheckTile(x, y + 1) && enemyManager.EnemyCheck(x, y + 1, false) == null)    //      //  //
                    {                                                                                   //      //  //  Move if available
                        y++;                                                                            //      //  //
                    }                                                                                   //      //  //
                    else if (enemyManager.EnemyCheck(x, y + 1, false) != null)                          //      //      //
                    {                                                                                   //      //      //  Attack if appropriate
                        Attack(enemyManager.EnemyCheck(x, y + 1, true));                                //      //      //
                    }                                                                                   //      //      //
                    break;                                                                              //      //
                case ConsoleKey.A:                                                                      //  //  Moving Left
                case ConsoleKey.LeftArrow:                                                              //  //
                    if (map.CheckTile(x-1, y) && enemyManager.EnemyCheck(x - 1, y, false) == null)      //  //  //
                    {                                                                                   //  //  //  Move if available
                        x--;                                                                            //  //  //
                    }                                                                                   //  //  //
                    else if (enemyManager.EnemyCheck(x - 1, y, false) != null)                          //  //      //
                    {                                                                                   //  //      //  Attack if appropriate
                        Attack(enemyManager.EnemyCheck(x - 1, y, true));                                //  //      //
                    }                                                                                   //  //      //
                    break;                                                                              //  //
                case ConsoleKey.D:                                                                      //      //  Moving Right
                case ConsoleKey.RightArrow:                                                             //      //
                    if (map.CheckTile(x + 1, y) && enemyManager.EnemyCheck(x + 1, y, false) == null)    //      //  //
                    {                                                                                   //      //  //  Move if available
                        x++;                                                                            //      //  //
                    }                                                                                   //      //  //
                    else if (enemyManager.EnemyCheck(x + 1, y, false) != null)                          //      //      //
                    {                                                                                   //      //      //  Attack if appropriate
                        Attack(enemyManager.EnemyCheck(x + 1, y, true));                                //      //      //
                    }                                                                                   //      //      //
                    break;                                                                              //      //
            }                                                                                           //
            if(itemManager.ItemChecks(x,y) != null)                 //
            {                                                       //  Pick up item if on item space
                itemManager.PickUp(itemManager.ItemChecks(x,y));    //
            }                                                       //
        }

        public bool PlayerCheck(int x, int y)   //returns true if the provided coordinates are the player's coordinates
        {
            bool check = false;
            if (this.x == x && this.y == y) check = true;
            return check;
        }

        public void Attack(Enemy enemy) //Attacks the provided enemy and gives the interaction message
        {
            enemy.TakeDMG(ATK);
            if (enemy.HP > 0) gManager.setMessage("Player attacked " + enemy.name);
            else gManager.setMessage("Player killed " + enemy.name);
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

    }
}
