using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Enemy : GameCharacter
    {
        private Player player;

        private EnemyType type;

        private bool moved;

        private string name;

        private int sightRange = 5;


        public Enemy(int x, int y, EnemyType type, Map map, Player player, EnemyManager enemyManager, Render rend, GameManager manager) : base(x, y, type.GetHP(), type.GetATK(), type.GetSprite(), map, enemyManager, type.GetColor(), rend, manager)
        {
            this.type = type;
            this.player = player;
            name = this.type.GetName();
        }

        public void Update(Random random)
        {
            if (alive)
            {
                moved = false;
                
                if(player.isPlayerAt(x,y-1) || player.isPlayerAt(x, y + 1) || player.isPlayerAt(x-1, y) || player.isPlayerAt(x+1, y))       //
                {                                                                                                                           //
                    AttackPlayer(player);                                                                                                   //  Enemy uses turn to attack player if they're adjacent
                    moved = true;                                                                                                           //
                }                                                                                                                           //

                while (moved == false)
                {
                    int Dir = random.Next(4);                                                                                                               //
                    if (type.GetEnemyType() == EnemyType.Type.slime || CanSeePlayer() == false)                                                             //
                    {                                                                                                                                       //
                        switch (Dir)                                                                                                                        //
                        {                                                                                                                                   //
                            case 0:                                                                                                                         //
                                if (map.isFloorAt(x, y - 1) && player.isPlayerAt(x, y - 1) == false && enemyManager.isEnemyAt(x, y - 1, false) == null)     //
                                {                                                                                                                           //
                                    y--;                                                                                                                    //
                                    moved = true;                                                                                                           //
                                }                                                                                                                           //
                                break;                                                                                                                      //
                            case 1:                                                                                                                         //
                                if (map.isFloorAt(x, y + 1) && player.isPlayerAt(x, y + 1) == false && enemyManager.isEnemyAt(x, y + 1, false) == null)     //
                                {                                                                                                                           //
                                    y++;                                                                                                                    //
                                    moved = true;                                                                                                           //
                                }                                                                                                                           //
                                break;                                                                                                                      //  Slimes and Goblins/Kobolds who are far away from the player go in random directions
                            case 2:                                                                                                                         //
                                if (map.isFloorAt(x - 1, y) && player.isPlayerAt(x - 1, y) == false && enemyManager.isEnemyAt(x - 1, y, false) == null)     //
                                {                                                                                                                           //
                                    x--;                                                                                                                    //
                                    moved = true;                                                                                                           //
                                }                                                                                                                           //
                                break;                                                                                                                      //
                            case 3:                                                                                                                         //
                                if (map.isFloorAt(x + 1, y) && player.isPlayerAt(x + 1, y) == false && enemyManager.isEnemyAt(x + 1, y, false) == null)     //
                                {                                                                                                                           //
                                    x++;                                                                                                                    //
                                    moved = true;                                                                                                           //
                                }                                                                                                                           //
                                break;                                                                                                                      //
                        }                                                                                                                                   //
                    }                                                                                                                                       //
                    else                                                                                                                                                                    //
                    {                                                                                                                                                                       //
                        int deltaX = player.GetX() - x;                                                                                                                                          //  Goblin or Kobold who is close to player
                        int deltaY = player.GetY() - y;                                                                                                                                          //
                        switch (type.GetEnemyType())                                                                                                                                                  //
                        {                                                                                                                                                                   //
                            case EnemyType.Type.goblin: //chase                                                                                                                             //  //
                                if(deltaX >= 0 && deltaY >= 0)                                                                                                                              //  //
                                {                                                                                                                                                           //  //
                                    if(deltaX >= deltaY && map.isFloorAt(x + 1, y) && player.isPlayerAt(x + 1, y) == false && enemyManager.isEnemyAt(x + 1, y, false) == null)            //  //
                                    {                                                                                                                                                       //  //
                                        x++;                                                                                                                                                //  //                                        
                                    }                                                                                                                                                       //  //
                                    else if(map.isFloorAt(x, y + 1) && player.isPlayerAt(x, y + 1) == false && enemyManager.isEnemyAt(x, y + 1, false) == null)                           //  //
                                    {                                                                                                                                                       //  //                                        
                                        y++;                                                                                                                                                //  //                                        
                                    }                                                                                                                                                       //  //  Goblin chases Player
                                }else if (deltaX >= 0 && deltaY < 0)                                                                                                                        //  //
                                {                                                                                                                                                           //  //
                                    if (deltaX >= deltaY * -1 && map.isFloorAt(x + 1, y) && player.isPlayerAt(x + 1, y) == false && enemyManager.isEnemyAt(x + 1, y, false) == null)      //  //
                                    {                                                                                                                                                       //  //
                                        x++;                                                                                                                                                //  //
                                    }                                                                                                                                                       //  //
                                    else if (map.isFloorAt(x, y - 1) && player.isPlayerAt(x, y - 1) == false && enemyManager.isEnemyAt(x, y - 1, false) == null)                          //  //
                                    {                                                                                                                                                       //  //
                                        y--;                                                                                                                                                //  //                                        
                                    }                                                                                                                                                       //  //
                                }else if (deltaX < 0 && deltaY >= 0)                                                                                                                        //  //
                                {                                                                                                                                                           //  //
                                    if (deltaX * -1 >= deltaY && map.isFloorAt(x - 1, y) && player.isPlayerAt(x - 1, y) == false && enemyManager.isEnemyAt(x - 1, y, false) == null)      //  //
                                    {                                                                                                                                                       //  //
                                        x--;                                                                                                                                                //  //
                                    }                                                                                                                                                       //  //
                                    else if (map.isFloorAt(x, y + 1) && player.isPlayerAt(x, y + 1) == false && enemyManager.isEnemyAt(x, y + 1, false) == null)                          //  //
                                    {                                                                                                                                                       //  //
                                        y++;                                                                                                                                                //  //
                                    }                                                                                                                                                       //  //
                                }                                                                                                                                                           //  //
                                if (deltaX < 0 && deltaY < 0)                                                                                                                               //  //
                                {                                                                                                                                                           //  //
                                    if (deltaX * -1 >= deltaY * -1 && map.isFloorAt(x - 1, y) && player.isPlayerAt(x - 1, y) == false && enemyManager.isEnemyAt(x - 1, y, false) == null) //  //
                                    {                                                                                                                                                       //  //
                                        x--;                                                                                                                                                //  //
                                    }                                                                                                                                                       //  //
                                    else if (map.isFloorAt(x, y - 1) && player.isPlayerAt(x, y - 1) == false && enemyManager.isEnemyAt(x, y - 1, false) == null)                          //  //
                                    {                                                                                                                                                       //  //
                                        y--;                                                                                                                                                //  //
                                    }                                                                                                                                                       //  //
                                }                                                                                                                                                           //  //
                                break;                                                                                                                                                      //  //
                            case EnemyType.Type.kobold: //flee                                                                                                                              //      //
                                if (deltaX < 0 && deltaY < 0)                                                                                                                               //      //
                                {                                                                                                                                                           //      //
                                    if (deltaX * -1 >= deltaY * -1 && map.isFloorAt(x + 1, y) && player.isPlayerAt(x + 1, y) == false && enemyManager.isEnemyAt(x + 1, y, false) == null) //      //
                                    {                                                                                                                                                       //      //
                                        x++;                                                                                                                                                //      //
                                    }                                                                                                                                                       //      //
                                    else if (map.isFloorAt(x, y + 1) && player.isPlayerAt(x, y + 1) == false && enemyManager.isEnemyAt(x, y + 1, false) == null)                          //      //
                                    {                                                                                                                                                       //      //
                                        y++;                                                                                                                                                //      //
                                    }                                                                                                                                                       //      //
                                }                                                                                                                                                           //      //
                                else if (deltaX < 0 && deltaY >= 0)                                                                                                                         //      //
                                {                                                                                                                                                           //      //
                                    if (deltaX * -1 >= deltaY && map.isFloorAt(x + 1, y) && player.isPlayerAt(x + 1, y) == false && enemyManager.isEnemyAt(x + 1, y, false) == null)      //      //
                                    {                                                                                                                                                       //      //
                                        x++;                                                                                                                                                //      //
                                    }                                                                                                                                                       //      //
                                    else if (map.isFloorAt(x, y - 1) && player.isPlayerAt(x, y - 1) == false && enemyManager.isEnemyAt(x, y - 1, false) == null)                          //      //
                                    {                                                                                                                                                       //      //
                                        y--;                                                                                                                                                //      //
                                    }                                                                                                                                                       //      //
                                }                                                                                                                                                           //      //
                                else if (deltaX >= 0 && deltaY < 0)                                                                                                                         //      //  Kobold runs from Player
                                {                                                                                                                                                           //      //
                                    if (deltaX >= deltaY * -1 && map.isFloorAt(x - 1, y) && player.isPlayerAt(x - 1, y) == false && enemyManager.isEnemyAt(x - 1, y, false) == null)      //      //
                                    {                                                                                                                                                       //      //
                                        x--;                                                                                                                                                //      //
                                    }                                                                                                                                                       //      //
                                    else if (map.isFloorAt(x, y + 1) && player.isPlayerAt(x, y + 1) == false && enemyManager.isEnemyAt(x, y + 1, false) == null)                          //      //
                                    {                                                                                                                                                       //      //
                                        y++;                                                                                                                                                //      //
                                    }                                                                                                                                                       //      //
                                }                                                                                                                                                           //      //
                                if (deltaX >= 0 && deltaY >= 0)                                                                                                                             //      //
                                {                                                                                                                                                           //      //
                                    if (deltaX >= deltaY && map.isFloorAt(x - 1, y) && player.isPlayerAt(x - 1, y) == false && enemyManager.isEnemyAt(x - 1, y, false) == null)           //      //
                                    {                                                                                                                                                       //      //
                                        x--;                                                                                                                                                //      //
                                    }                                                                                                                                                       //      //
                                    else if (map.isFloorAt(x, y - 1) && player.isPlayerAt(x, y - 1) == false && enemyManager.isEnemyAt(x, y - 1, false) == null)                          //      //
                                    {                                                                                                                                                       //      //
                                        y--;                                                                                                                                                //      //
                                    }                                                                                                                                                       //      //
                                }                                                                                                                                                           //      //
                                break;                                                                                                                                                      //      //
                        }                                                                                                                                                                   //
                        moved = true;
                    }
                }
            }
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
            else if (manager.GetMessage() == "") manager.setMessage(name + " attacked Player");
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
