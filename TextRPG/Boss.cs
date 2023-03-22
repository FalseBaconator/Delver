using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Boss : Enemy
    {
        public Boss(int x, int y, Map map, Player player, EnemyManager enemyManager, ItemManager itemManager, Render rend, GameManager gameManager) : base(x, y, Constants.bossBaseHP, Constants.bossBaseAttack, Constants.bossSprite, Constants.bossName, map, player, enemyManager, Constants.bossColor, itemManager, rend, gameManager)
        {

        }

        public override void Update(Random random)
        {
            if (alive)
            {
                moved = false;

                if (player.isPlayerAt(x, y - 1) || player.isPlayerAt(x, y + 1) || player.isPlayerAt(x - 1, y) || player.isPlayerAt(x + 1, y))       //
                {                                                                                                                                   //
                    AttackPlayer(player);                                                                                                           //  Enemy uses turn to attack player if they're adjacent
                    moved = true;                                                                                                                   //
                }

                while (moved == false)
                {
                    int deltaX = player.GetX() - x;         //
                    int deltaY = player.GetY() - y;         //
                    targetX = x;                            //
                    targetY = y;                            //
                                                            //
                    if (deltaX >= 0 && deltaY >= 0)         //
                    {                                       //
                        if (deltaX >= deltaY)               //
                        {                                   //
                            targetX++;                      //
                        }                                   //
                        else                                //
                        {                                   //
                            targetY++;                      //
                        }                                   //
                    }                                       //
                    else if (deltaX >= 0 && deltaY < 0)     //
                    {                                       //
                        if (deltaX >= deltaY * -1)          //
                        {                                   //  Pick a direction to travel in based on player's position
                            targetX++;                      //  Chase Player
                        }                                   //
                        else                                //
                        {                                   //
                            targetY--;                      //
                        }                                   //
                    }                                       //
                    else if (deltaX < 0 && deltaY >= 0)     //
                    {                                       //
                        if (deltaX * -1 >= deltaY)          //
                        {                                   //
                            targetX--;                      //
                        }                                   //
                        else                                //
                        {                                   //
                            targetY++;                      //
                        }                                   //
                    }                                       //
                    else if (deltaX < 0 && deltaY < 0)      //
                    {                                       //
                        if (deltaX * -1 >= deltaY * -1)     //
                        {                                   //
                            targetX--;                      //
                        }                                   //
                        else                                //
                        {                                   //
                            targetY--;                      //
                        }                                   //
                    }                                       //


                    if (map.isFloorAt(targetX, targetY) && enemyManager.EnemyAt(targetX, targetY, false) == null && itemManager.ItemAt(targetX, targetY) == null && player.isPlayerAt(targetX, targetY) == false)
                    {                   //
                        moved = true;   //
                        x = targetX;    //  Move if open space
                        y = targetY;    //
                    }                   //
                    else                    //
                    {                       //
                        RandomMove(random); //  If targe space is blocked, just go random
                    }                       //
                }

            }
        }

        public override void TakeDMG(int DMG)
        {
            base.TakeDMG(DMG);
            if(GetHealth() <= 0)
            {
                manager.EndGame(true);
            }
        }

    }
}
