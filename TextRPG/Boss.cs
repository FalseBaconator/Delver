using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Boss : Enemy
    {
        public Boss(Position pos, Map map, Player player, EnemyManager enemyManager, ItemManager itemManager, Render rend, GameManager gameManager, Hud hud, Exit exit, SoundManager soundManager) : base(pos, GameManager.constants.bossBaseHP, GameManager.constants.bossBaseAttack, GameManager.constants.bossSprite, GameManager.constants.bossName, map, player, enemyManager, itemManager, rend, gameManager, hud, exit, 0, 0, soundManager)
        {

        }


        public override void Update()
        {
            if (alive)
            {
                if (player.isPlayerAt(new Position(pos.x, pos.y - 1)) || player.isPlayerAt(new Position(pos.x, pos.y + 1)) || player.isPlayerAt(new Position(pos.x - 1, pos.y)) || player.isPlayerAt(new Position(pos.x + 1, pos.y)))       //
                {                                                                                                                                   //
                    AttackPlayer(player);                                                                                                           //  Enemy uses turn to attack player if they're adjacent
                }
                else
                {
                    int deltaX = player.GetPos().x - pos.x;         //
                    int deltaY = player.GetPos().y - pos.y;         //  Prepare to move
                    targetPos = pos;

                    //---------------------------Chose a direction (chase)
                    if (deltaX > 0 && deltaY > 0)
                    {
                        if (deltaX >= deltaY)
                        {
                            if (IsSpaceAvailable(new Position(pos.x + 1, pos.y)))
                                targetPos.x++;
                            else if (IsSpaceAvailable(new Position(pos.x, pos.y + 1)))
                                targetPos.y++;
                        }
                        else
                        {
                            if (IsSpaceAvailable(new Position(pos.x, pos.y + 1)))
                                targetPos.y++;
                            else if (IsSpaceAvailable(new Position(pos.x + 1, pos.y)))
                                targetPos.x++;
                        }
                    }
                    else if (deltaX > 0 && deltaY < 0)
                    {
                        if (deltaX >= deltaY * -1)
                        {
                            if (IsSpaceAvailable(new Position(pos.x + 1, pos.y)))
                                targetPos.x++;
                            else if (IsSpaceAvailable(new Position(pos.x, pos.y - 1)))
                                targetPos.y--;
                        }
                        else
                        {
                            if (IsSpaceAvailable(new Position(pos.x, pos.y - 1)))
                                targetPos.y--;
                            else if (IsSpaceAvailable(new Position(pos.x + 1, pos.y)))
                                targetPos.x++;
                        }
                    }
                    else if (deltaX < 0 && deltaY > 0)
                    {
                        if (deltaX * -1 >= deltaY)
                        {
                            if (IsSpaceAvailable(new Position(pos.x - 1, pos.y)))
                                targetPos.x--;
                            else if (IsSpaceAvailable(new Position(pos.x, pos.y + 1)))
                                targetPos.y++;
                        }
                        else
                        {
                            if (IsSpaceAvailable(new Position(pos.x, pos.y + 1)))
                                targetPos.y++;
                            else if (IsSpaceAvailable(new Position(pos.x - 1, pos.y)))
                                targetPos.x--;
                        }
                    }
                    else if (deltaX < 0 && deltaY < 0)
                    {
                        if (deltaX * -1 >= deltaY * -1)
                        {
                            if (IsSpaceAvailable(new Position(pos.x - 1, pos.y)))
                                targetPos.x--;
                            else if (IsSpaceAvailable(new Position(pos.x, pos.y - 1)))
                                targetPos.y--;
                        }
                        else
                        {
                            if (IsSpaceAvailable(new Position(pos.x, pos.y - 1)))
                                targetPos.y--;
                            else if (IsSpaceAvailable(new Position(pos.x - 1, pos.y)))
                                targetPos.x--;
                        }
                    }
                    else if (deltaX == 0)
                    {
                        if (deltaY < 0)
                        {
                            if (IsSpaceAvailable(new Position(pos.x, pos.y - 1)))
                                targetPos.y--;
                            else if (IsSpaceAvailable(new Position(pos.x + 1, pos.y)))
                                targetPos.x++;
                            else
                                targetPos.x--;
                        }
                        else
                        {
                            if (IsSpaceAvailable(new Position(pos.x, pos.y + 1)))
                                targetPos.y++;
                            else if (IsSpaceAvailable(new Position(pos.x + 1, pos.y)))
                                targetPos.x++;
                            else
                                targetPos.x--;
                        }
                    }
                    else if (deltaY == 0)
                    {
                        if (deltaX < 0)
                        {
                            if (IsSpaceAvailable(new Position(pos.x - 1, pos.y)))
                                targetPos.x--;
                            else if (IsSpaceAvailable(new Position(pos.x, pos.y + 1)))
                                targetPos.y++;
                            else
                                targetPos.y--;
                        }
                        else
                        {
                            if (IsSpaceAvailable(new Position(pos.x + 1, pos.y)))
                                targetPos.x++;
                            else if (IsSpaceAvailable(new Position(pos.x, pos.y + 1)))
                                targetPos.y++;
                            else
                                targetPos.y--;
                        }
                    }
                    //---------------------------Direction Chosen (chase)

                    if (IsSpaceAvailable(targetPos))
                    {                   //
                        pos = targetPos;
                    }                   //
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
