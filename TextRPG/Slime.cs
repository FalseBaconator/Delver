using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextRPG
{
    internal class Slime : Enemy
    {

        public Slime(int x, int y, Map map, Player player, EnemyManager enemyManager, ItemManager itemManager, Render rend, GameManager gameManager) : base(x, y, Constants.slimeBaseHP, Constants.slimeBaseAttack, Constants.slimeSprite, Constants.slimeName, map, player, enemyManager, Constants.slimeColor, itemManager, rend, gameManager)
        {

        }

        public override void Update()
        {
            if (alive)
            {

                if (player.isPlayerAt(x, y - 1) || player.isPlayerAt(x, y + 1) || player.isPlayerAt(x - 1, y) || player.isPlayerAt(x + 1, y))       //
                {                                                                                                                                   //
                    AttackPlayer(player);                                                                                                           //  Enemy uses turn to attack player if they're adjacent
                }                                                                                                                                   //
                else                //
                {                   //  Move in a random direction if hasn't attacked
                    RandomMove();   //
                }                   //

            }
        }
    }
}
