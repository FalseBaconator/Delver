using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Kobold : Enemy
    {

        public Kobold(int x, int y, Map map, Player player, EnemyManager enemyManager, ItemManager itemManager, Render rend, GameManager gameManager) : base(x, y, Constants.koboldBaseHP, Constants.koboldBaseAttack, Constants.koboldSprite, Constants.koboldName, map, player, enemyManager, Constants.koboldColor, itemManager, rend, gameManager)
        {

        }

        public override void Update(Random random)
        {
            if (alive)
            {

                if (player.isPlayerAt(x, y - 1) || player.isPlayerAt(x, y + 1) || player.isPlayerAt(x - 1, y) || player.isPlayerAt(x + 1, y))       //
                {                                                                                                                                   //
                    AttackPlayer(player);                                                                                                           //  Enemy uses turn to attack player if they're adjacent
                }                                                                                                                                   //
                else if (CanSeePlayer() == false)       //
                {                                       //  Player too far away, move randomly
                    RandomMove(random);                 //
                }                                       //
                else
                {                                           //
                    int deltaX = player.GetX() - x;         //
                    int deltaY = player.GetY() - y;         //  Prepare to Move
                    targetX = x;                            //
                    targetY = y;                            //

                    //---------------------------Chose a direction (run)
                    if (deltaX > 0 && deltaY > 0)
                    {
                        if (deltaX >= deltaY)
                        {
                            if (IsSpaceAvailable(x - 1, y))
                                targetX--;
                            else if (IsSpaceAvailable(x, y - 1))
                                targetY--;
                        }
                        else
                        {
                            if (IsSpaceAvailable(x, y-1))
                                targetY--;
                            else if (IsSpaceAvailable(x-1, y))
                                targetX--;
                        }
                    }
                    else if (deltaX > 0 && deltaY < 0)
                    {
                        if (deltaX >= deltaY * -1)
                        {
                            if (IsSpaceAvailable(x - 1, y))
                                targetX--;
                            else if (IsSpaceAvailable(x, y + 1))
                                targetY++;
                        }
                        else
                        {
                            if (IsSpaceAvailable(x, y + 1))
                                targetY++;
                            else if (IsSpaceAvailable(x - 1, y))
                                targetX--;
                        }
                    }
                    else if (deltaX < 0 && deltaY > 0)
                    {
                        if (deltaX * -1 >= deltaY)
                        {
                            if (IsSpaceAvailable(x + 1, y))
                                targetX++;
                            else if (IsSpaceAvailable(x, y - 1))
                                targetY--;
                        }
                        else
                        {
                            if (IsSpaceAvailable(x, y - 1))
                                targetY--;
                            else if (IsSpaceAvailable(x + 1, y))
                                targetX++;
                        }
                    }
                    else if (deltaX < 0 && deltaY < 0)
                    {
                        if (deltaX * -1 >= deltaY * -1)
                        {
                            if (IsSpaceAvailable(x + 1, y))
                                targetX++;
                            else if (IsSpaceAvailable(x, y + 1))
                                targetY++;
                        }
                        else
                        {
                            if (IsSpaceAvailable(x, y + 1))
                                targetY++;
                            else if (IsSpaceAvailable(x + 1, y))
                                targetX++;
                        }
                    }
                    else if(deltaX == 0)
                    {
                        if(deltaY < 0)
                        {
                            if (IsSpaceAvailable(x, y + 1))
                                targetY++;
                            else if (IsSpaceAvailable(x - 1, y))
                                targetX--;
                            else
                                targetX++;
                        }
                        else
                        {
                            if (IsSpaceAvailable(x, y - 1))
                                targetY--;
                            else if (IsSpaceAvailable(x - 1, y))
                                targetX--;
                            else
                                targetX++;
                        }
                    }else if(deltaY == 0)
                    {
                        if (deltaX < 0)
                        {
                            if (IsSpaceAvailable(x+1, y))
                                targetX++;
                            else if (IsSpaceAvailable(x, y-1))
                                targetY--;
                            else
                                targetY++;
                        }
                        else
                        {
                            if (IsSpaceAvailable(x-1, y))
                                targetX--;
                            else if (IsSpaceAvailable(x, y-1))
                                targetY--;
                            else
                                targetY++;
                        }
                    }
                    //---------------------------Direction chosen (run)


                    if (IsSpaceAvailable(targetX, targetY))
                    {                   //
                        x = targetX;    //  Move if open space
                        y = targetY;    //
                    }                   //
                }
            }
        }

    }
}
